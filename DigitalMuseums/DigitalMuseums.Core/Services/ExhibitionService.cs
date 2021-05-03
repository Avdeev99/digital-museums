using System;
using System.Collections.Generic;
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
        }

        public async Task CreateAsync(CreateExhibitionDto createExhibitionDto)
        {
            var exhibition = _mapper.Map<Exhibition>(createExhibitionDto);
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
            
            UpdateExhibitionItem(exhibition, updateExhibitionDto);
            
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
        
        private void UpdateExhibitionItem(Exhibition exhibit, UpdateExhibitionDto updateExhibitDto)
        {
            exhibit.Name = updateExhibitDto.Name;
            exhibit.Description = updateExhibitDto.Description;
            exhibit.Tags = updateExhibitDto.Tags;
            
            // TODO: update exhibits
            
            if (updateExhibitDto.ImagesData != null)
            {
                exhibit.Images = null;
                _imageService.AddAndUpload(updateExhibitDto.ImagesData);
            }
        }
    }
}