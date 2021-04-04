using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO.Exhibit;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class ExhibitService : IExhibitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterPipeline<Exhibit, FilterExhibitsDto> _exhibitFilterPipeline;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        
        private readonly IBaseRepository<Exhibit> _exhibitRepository;

        public ExhibitService(
            IUnitOfWork unitOfWork,
            IFilterPipeline<Exhibit, FilterExhibitsDto> exhibitFilterPipeline,
            IImageService imageService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _exhibitFilterPipeline = exhibitFilterPipeline;
            _imageService = imageService;
            _mapper = mapper;

            _exhibitRepository = unitOfWork.GetRepository<Exhibit>();
        }

        public async Task CreateAsync(CreateExhibitDto createExhibitDto)
        {
            var exhibit = _mapper.Map<Exhibit>(createExhibitDto);
            var exhibitResult = _exhibitRepository.Create(exhibit);
            await _unitOfWork.SaveChangesAsync();

            createExhibitDto.ImagesData.ExhibitId = exhibitResult.Id;
            _imageService.AddAndUpload(createExhibitDto.ImagesData);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateExhibitDto updateExhibitDto)
        {
            var exhibit = await _exhibitRepository.GetAsync(
                exhibit => exhibit.Id == updateExhibitDto.Id,
                new List<Expression<Func<Exhibit, object>>>
                {
                    x => x.Images
                },
                TrackingState.Enabled);
            if (exhibit == null || exhibit.IsDeleted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }
            
            UpdateExhibitItem(exhibit, updateExhibitDto);
            
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ExhibitItem> GetAsync(int id)
        {
            var exhibit = await _exhibitRepository.GetAsync(
                exhibit => exhibit.Id == id,
                new List<Expression<Func<Exhibit, object>>>
                {
                    exhibit => exhibit.Exhibitions,
                    exhibit => exhibit.Images
                });
            if (exhibit == null || exhibit.IsDeleted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }

            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<ExhibitItem>(exhibit);

            return result;
        }

        public async Task<List<FilteredExhibitItem>> GetFilteredAsync(FilterExhibitsDto filter)
        {
            var query = _exhibitFilterPipeline.BuildQuery(filter);
            var exhibits = await _exhibitRepository.GetAllAsync(
                query,
                new List<Expression<Func<Exhibit, object>>>
                {
                    museum => museum.Images
                });

            var result = _mapper.Map<List<FilteredExhibitItem>>(exhibits);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var exhibit = await _exhibitRepository.GetAsync(museum => museum.Id == id, TrackingState.Enabled);
            if (exhibit == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }

            exhibit.IsDeleted = true;

            await _unitOfWork.SaveChangesAsync();
        }
        
        private void UpdateExhibitItem(Exhibit exhibit, UpdateExhibitDto updateExhibitDto)
        {
            exhibit.Name = updateExhibitDto.Name;
            exhibit.Description = updateExhibitDto.Description;
            exhibit.Author = updateExhibitDto.Author;
            exhibit.Date = updateExhibitDto.Date;
            exhibit.Tags = updateExhibitDto.Tags;
            
            if (updateExhibitDto.ImagesData != null)
            {
                exhibit.Images = null;
                _imageService.AddAndUpload(updateExhibitDto.ImagesData);
            }
        }
    }
}