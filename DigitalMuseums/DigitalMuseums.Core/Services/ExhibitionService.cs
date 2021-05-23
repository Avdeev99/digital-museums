using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO.Exhibition;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Core.Services.Contracts.Providers;

namespace DigitalMuseums.Core.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterPipeline<Exhibition, FilterExhibitionsDto> _exhibitionFilterPipeline;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly ILoggedInPersonProvider _loggedInPersonProvider;
        
        private readonly IBaseRepository<Exhibition> _exhibitionRepository;
        private readonly IBaseRepository<Exhibit> _exhibitRepository;
        private readonly IBaseRepository<User> _userRepository;

        public ExhibitionService(
            IUnitOfWork unitOfWork,
            IFilterPipeline<Exhibition, FilterExhibitionsDto> exhibitionFilterPipeline,
            IImageService imageService,
            IMapper mapper,
            ILoggedInPersonProvider loggedInPersonProvider)
        {
            _unitOfWork = unitOfWork;
            _exhibitionFilterPipeline = exhibitionFilterPipeline;
            _imageService = imageService;
            _mapper = mapper;
            _loggedInPersonProvider = loggedInPersonProvider;

            _exhibitionRepository = unitOfWork.GetRepository<Exhibition>();
            _exhibitRepository = unitOfWork.GetRepository<Exhibit>();
            _userRepository = unitOfWork.GetRepository<User>();
        }

        public async Task CreateAsync(CreateExhibitionDto createExhibitionDto)
        {
            var userId = _loggedInPersonProvider.GetUserId();
            var includes = new List<Expression<Func<User, object>>>
            {
                u => u.Museum
            };
            var user = await _userRepository.GetAsync(u => u.Id == userId, includes);
            if (user?.Museum == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.UserWithoutMuseumCode);
            }

            var exhibition = _mapper.Map<Exhibition>(createExhibitionDto);
            var exhibits = await _exhibitRepository.GetAllAsync(e => createExhibitionDto.ExhibitIds.Contains(e.Id), TrackingState.Enabled);
            exhibition.Exhibits = exhibits;
            exhibition.MuseumId = user.Museum.Id;

            await CheckSameNameExistenceAsync(exhibition.Name, exhibition.Id, exhibition.MuseumId);

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

            await CheckSameNameExistenceAsync(updateExhibitionDto.Name, exhibition.Id, exhibition.MuseumId);

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

        private async Task CheckSameNameExistenceAsync(string name, int exhibitionId, int museumId)
        {
            var isExisted = await _exhibitionRepository.IsExistAsync(x =>
                x.Name == name && x.Id != exhibitionId && x.MuseumId == museumId);

            if (isExisted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.ExhibitionWithSameNameExistCode);
            }
        }
    }
}