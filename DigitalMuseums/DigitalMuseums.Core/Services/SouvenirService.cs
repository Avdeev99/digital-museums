using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO.Souvenir;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class SouvenirService : ISouvenirService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        
        private readonly IBaseRepository<Souvenir> _souvenirRepository;

        public SouvenirService(
            IUnitOfWork unitOfWork,
            IImageService imageService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
            
            _souvenirRepository = unitOfWork.GetRepository<Souvenir>();
        }
        
        public async Task CreateAsync(CreateSouvenirDto createSouvenirDto)
        {
            var souvenir = _mapper.Map<Souvenir>(createSouvenirDto);
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
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
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
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }

            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<SouvenirItem>(souvenir);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var souvenir = await _souvenirRepository.GetAsync(x => x.Id == id, TrackingState.Enabled);
            if (souvenir == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
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