using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO.Souvenir;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Core.Services.Contracts.Providers;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Services
{
    public class SouvenirService : ISouvenirService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IOrderedFilterPipeline<Souvenir, FilterSouvenirsDto> _souvenirFilterPipeline;
        private readonly IMapper _mapper;
        private readonly ILoggedInPersonProvider _loggedInPersonProvider;

        private readonly IBaseRepository<Souvenir> _souvenirRepository;
        private readonly IBaseRepository<User> _userRepository;

        public SouvenirService(
            IUnitOfWork unitOfWork,
            IImageService imageService,
            IOrderedFilterPipeline<Souvenir, FilterSouvenirsDto> souvenirFilterPipeline,
            IMapper mapper,
            ILoggedInPersonProvider loggedInPersonProvider)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _souvenirFilterPipeline = souvenirFilterPipeline;
            _mapper = mapper;
            _loggedInPersonProvider = loggedInPersonProvider;

            _souvenirRepository = unitOfWork.GetRepository<Souvenir>();
            _userRepository = unitOfWork.GetRepository<User>();
        }
        
        public async Task CreateAsync(CreateSouvenirDto createSouvenirDto)
        {
            var userId = _loggedInPersonProvider.GetUserId();
            var includes = new List<Expression<Func<User, object>>>
            {
                u => u.Museum
            };
            var user = await _userRepository.GetAsync(u => u.Id == userId, includes);
            if (user?.Museum == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.UserWithoutMuseum);
            }

            var souvenir = _mapper.Map<Souvenir>(createSouvenirDto);
            souvenir.MuseumId = user.Museum.Id;
            var souvenirResult = _souvenirRepository.Create(souvenir);
            await _unitOfWork.SaveChangesAsync();

            createSouvenirDto.ImagesData.SouvenirId = souvenirResult.Id;
            _imageService.AddAndUpload(createSouvenirDto.ImagesData);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateSouvenirDto updateSouvenirDto)
        {
            var souvenir = await _souvenirRepository.GetAsync(
                x => x.Id == updateSouvenirDto.Id,
                new List<Expression<Func<Souvenir, object>>>
                {
                    x => x.Images
                },
                TrackingState.Enabled);
            if (souvenir == null || souvenir.IsDeleted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.SouvenirNotFoundCode);
            }
            
            UpdateInternal(souvenir, updateSouvenirDto);
            
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<SouvenirItem> GetAsync(int id)
        {
            var souvenir = await _souvenirRepository.GetAsync(
                x => x.Id == id,
                new List<Expression<Func<Souvenir, object>>>
                {
                    x => x.Images
                });
            if (souvenir == null || souvenir.IsDeleted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.SouvenirNotFoundCode, StatusCodes.Status404NotFound);
            }

            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<SouvenirItem>(souvenir);

            return result;
        }

        public async Task<List<FilteredSouvenirItem>> GetFilteredAsync(FilterSouvenirsDto filter)
        {
            var query = _souvenirFilterPipeline.BuildQuery(filter);
            var souvenirs = await _souvenirRepository.GetAllAsync(
                query,
                new List<Expression<Func<Souvenir, object>>>
                {
                    museum => museum.Images
                });
            var orderByQuery = _souvenirFilterPipeline.BuildOrderByQuery(filter);

            var result = _mapper.Map<List<FilteredSouvenirItem>>(orderByQuery(souvenirs).ToList());

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var souvenir = await _souvenirRepository.GetAsync(x => x.Id == id, TrackingState.Enabled);
            if (souvenir == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.SouvenirNotFoundCode, StatusCodes.Status404NotFound);
            }

            souvenir.IsDeleted = true;

            await _unitOfWork.SaveChangesAsync();
        }
        
        private void UpdateInternal(Souvenir souvenir, UpdateSouvenirDto updateSouvenirDto)
        {
            souvenir.Name = updateSouvenirDto.Name;
            souvenir.Description = updateSouvenirDto.Description;
            souvenir.Price = updateSouvenirDto.Price;
            souvenir.AvailableUnits = updateSouvenirDto.AvailableUnits;
            souvenir.Tags = updateSouvenirDto.Tags;
            
            if (updateSouvenirDto.ImagesData != null)
            {
                souvenir.Images = null;
                _imageService.AddAndUpload(updateSouvenirDto.ImagesData);
            }
        }
    }
}