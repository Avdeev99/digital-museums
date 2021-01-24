using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class MuseumService : IMuseumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Museum> _museumRepository;
        private readonly IBaseRepository<User> _userRepository;

        public MuseumService(IUnitOfWork unitOfWork, IImageService imageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
            _museumRepository = unitOfWork.GetRepository<Museum>();
            _userRepository = unitOfWork.GetRepository<User>();
        }
        
        public void Create(MuseumDto museumDto)
        {
            var museum = _mapper.Map<Museum>(museumDto);
            var museumResult = _museumRepository.Create(museum);
            _unitOfWork.SaveChanges();
            
            museumDto.ImagesData.MuseumId = museumResult.Id;
            _imageService.AddAndUpload(museumDto.ImagesData);
            _unitOfWork.SaveChanges();
        }

        public async Task LinkUserAsync(LinkUserToMuseumDto linkUserToMuseumDto)
        {
            var isUserExist = await _userRepository.IsExistAsync(user => user.Id == linkUserToMuseumDto.UserId);
            if (!isUserExist)
            {
                throw new BusinessLogicException(BusinessErrorCodes.UserNotFoundCode);
            }
            
            var museum = await _museumRepository.GetAsync(m => m.Id == linkUserToMuseumDto.MuseumId, TrackingState.Enabled);
            if (museum == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.MuseumNotFoundCode);
            }

            museum.UserId = linkUserToMuseumDto.UserId;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}