using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.Constants;
using DigitalMuseums.Core.Domain.DTO.Museum;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Domain.Models.Location;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Services
{
    public class MuseumService : IMuseumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IOrderedFilterPipeline<Museum, FilterMuseumsDto> _museumFilterPipeline;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Museum> _museumRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<Location> _locationRepository;

        public MuseumService(
            IUnitOfWork unitOfWork,
            IImageService imageService,
            IOrderedFilterPipeline<Museum,FilterMuseumsDto> museumFilterPipeline,
            IMapper mapper,
            IBaseRepository<Role> roleRepository)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _museumFilterPipeline = museumFilterPipeline;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _museumRepository = unitOfWork.GetRepository<Museum>();
            _userRepository = unitOfWork.GetRepository<User>();
            _locationRepository = unitOfWork.GetRepository<Location>();
        }

        public async Task Create(CreateMuseumDto createMuseumDto)
        {
            var museum = _mapper.Map<Museum>(createMuseumDto);
            var museumResult = _museumRepository.Create(museum);
            await _unitOfWork.SaveChangesAsync();

            createMuseumDto.ImagesData.MuseumId = museumResult.Id;
            _imageService.AddAndUpload(createMuseumDto.ImagesData);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateMuseumDto museumDto)
        {
            var museum = await _museumRepository.GetAsync(
                museum => museum.Id == museumDto.Id,
                new List<Expression<Func<Museum, object>>>
                {
                    m => m.Location,
                    m => m.Images
                },
                TrackingState.Enabled);
            if (museum == null || museum.IsDeleted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }
            
            await UpdateMuseumItem(museum, museumDto);
            
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var museum = await _museumRepository.GetAsync(museum => museum.Id == id, TrackingState.Enabled);
            if (museum == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode, StatusCodes.Status404NotFound);
            }

            if (museum.UserId.HasValue)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumLinkedToUserCode);
            }

            museum.IsDeleted = true;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LinkUserAsync(LinkUserToMuseumDto linkUserToMuseumDto)
        {
            var user = await _userRepository.GetAsync(user => user.Id == linkUserToMuseumDto.UserId, TrackingState.Enabled);
            if (user == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.UserNotFoundCode);
            }

            var museum =
                await _museumRepository.GetAsync(m => m.Id == linkUserToMuseumDto.MuseumId, TrackingState.Enabled);
            if (museum == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }

            var role = await _roleRepository.GetAsync(r => r.Name == UserRoleConstants.MuseumOwner);
            if (role == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.RoleNotFoundCode);
            }

            var oldMuseum = await _museumRepository.GetAsync(m => m.UserId == linkUserToMuseumDto.UserId, TrackingState.Enabled);
            if (oldMuseum != null)
            {
                oldMuseum.UserId = null;
            }

            user.RoleId = role.Id;
            museum.UserId = linkUserToMuseumDto.UserId;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<MuseumItem> GetAsync(int id)
        {
            var museum = await _museumRepository.GetAsync(
                museum => museum.Id == id,
                new List<Expression<Func<Museum, object>>>
                {
                    museum => museum.Genre,
                    museum => museum.Location.City.Region.Country,
                    museum => museum.Images
                },
                TrackingState.Enabled);
            if (museum == null || museum.IsDeleted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode, StatusCodes.Status404NotFound);
            }

            museum.VisitedCount++;
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<MuseumItem>(museum);

            return result;
        }

        public async Task<List<FilteredMuseumItem>> GetFilteredAsync(FilterMuseumsDto filter)
        {
            var query = _museumFilterPipeline.BuildQuery(filter);
            var museums = await _museumRepository.GetAllAsync(
                query,
                new List<Expression<Func<Museum, object>>>
                {
                    museum => museum.Genre,
                    museum => museum.Images
                });
            var orderByQuery = _museumFilterPipeline.BuildOrderByQuery(filter);

            var result = _mapper.Map<List<FilteredMuseumItem>>(orderByQuery(museums).ToList());

            return result;
        }

        public async Task<List<BasePredefinedEntity>> GetBaseListAsync()
        {
            var museums = await _museumRepository.GetAllAsync(m => !m.IsDeleted);
            var result = _mapper.Map<List<BasePredefinedEntity>>(museums);

            return result;
        }

        public async Task<List<BasePredefinedEntity>> GetBaseListAsync(int userId)
        {
            var museums = await _museumRepository.GetAllAsync(m => m.UserId == userId);
            var result = _mapper.Map<List<BasePredefinedEntity>>(museums);

            return result;
        }

        private async Task UpdateMuseumItem(Museum museum, UpdateMuseumDto museumDto)
        {
            museum.GenreId = museumDto.GenreId;
            museum.Name = museumDto.Name;
            museum.Description = museumDto.Description;
            if (museumDto.ImagesData != null)
            {
                museum.Images = null;
                _imageService.AddAndUpload(museumDto.ImagesData);
            }
            
            if (museumDto.Address != museum.Location.Address || museumDto.CityId != museum.Location.CityId)
            {
                var location = await _locationRepository.GetAsync(location => location.Id == museum.LocationId, TrackingState.Enabled);
                location.Address = museumDto.Address;
                location.CityId = museumDto.CityId;
            }
        }
    }
}