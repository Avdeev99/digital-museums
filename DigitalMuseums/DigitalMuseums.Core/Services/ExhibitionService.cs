using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO.Exhibition;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterPipeline<Exhibition, FilterExhibitionsDto> _exhibitionFilterPipeline;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        
        private readonly IBaseRepository<Exhibition> _exhibitionRepository;
        private readonly IBaseRepository<Exhibit> _exhibitRepository;

        public ExhibitionService(
            IUnitOfWork unitOfWork,
            IFilterPipeline<Exhibition, FilterExhibitionsDto> exhibitionFilterPipeline,
            IImageService imageService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _exhibitionFilterPipeline = exhibitionFilterPipeline;
            _imageService = imageService;
            _mapper = mapper;
            
            _exhibitionRepository = unitOfWork.GetRepository<Exhibition>();
            _exhibitRepository = unitOfWork.GetRepository<Exhibit>();
        }

        public async Task CreateAsync(CreateExhibitionDto createExhibitionDto)
        {
            var exhibition = _mapper.Map<Exhibition>(createExhibitionDto);
            var exhibits = await _exhibitRepository.GetAllAsync(e => createExhibitionDto.ExhibitIds.Contains(e.Id), TrackingState.Enabled);
            exhibition.Exhibits = exhibits;

            var createdExhibition = _exhibitionRepository.Create(exhibition);
            await _unitOfWork.SaveChangesAsync();

            createExhibitionDto.ImagesData.ExhibitionId = createdExhibition.Id;
            _imageService.AddAndUpload(createExhibitionDto.ImagesData);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateExhibitionDto updateExhibitionDto)
        {
            var exhibition = await _exhibitionRepository.GetAsync(
                e => e.Id == updateExhibitionDto.Id,
                new List<Expression<Func<Exhibition, object>>>
                {
                    x => x.Images,
                    x => x.Exhibits
                },
                TrackingState.Enabled);
            if (exhibition == null || exhibition.IsDeleted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }
            
            await UpdateExhibitionItemAsync(exhibition, updateExhibitionDto);
            
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ExhibitionItem> GetAsync(int id)
        {
            var exhibition = await _exhibitionRepository.GetAsync(
                e => e.Id == id,
                new List<Expression<Func<Exhibition, object>>>
                {
                    e => e.Exhibits,
                    e => e.Images
                });
            if (exhibition == null || exhibition.IsDeleted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.ExhibitionNotFoundCode);
            }

            var result = _mapper.Map<ExhibitionItem>(exhibition);

            return result;
        }

        public async Task<List<FilteredExhibitionItem>> GetFilteredAsync(FilterExhibitionsDto filter)
        {
            var query = _exhibitionFilterPipeline.BuildQuery(filter);
            var exhibits = await _exhibitionRepository.GetAllAsync(
                query,
                new List<Expression<Func<Exhibition, object>>>
                {
                    e => e.Images
                });

            var result = _mapper.Map<List<FilteredExhibitionItem>>(exhibits);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var exhibition = await _exhibitionRepository.GetAsync(e => e.Id == id, TrackingState.Enabled);
            if (exhibition == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }

            exhibition.IsDeleted = true;

            await _unitOfWork.SaveChangesAsync();
        }
        
        private async Task UpdateExhibitionItemAsync(Exhibition exhibition, UpdateExhibitionDto updateExhibitDto)
        {
            exhibition.Name = updateExhibitDto.Name;
            exhibition.Description = updateExhibitDto.Description;
            exhibition.Tags = updateExhibitDto.Tags;

            var exhibits = await _exhibitRepository.GetAllAsync(e => updateExhibitDto.ExhibitIds.Contains(e.Id));

            var existedIds = exhibition.Exhibits.Select(x => x.Id);
            var exhibitsToAdd = exhibits.Where(e => !existedIds.Contains(e.Id)).ToList();
            var newExhibitIds = exhibits.Select(x => x.Id);

            exhibition.Exhibits.RemoveAll(e => !newExhibitIds.Contains(e.Id));
            exhibition.Exhibits.AddRange(exhibitsToAdd);

            if (updateExhibitDto.ImagesData != null)
            {
                exhibition.Images = null;
                _imageService.AddAndUpload(updateExhibitDto.ImagesData);
            }
        }
    }
}