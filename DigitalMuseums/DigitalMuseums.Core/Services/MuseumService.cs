using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class MuseumService : IMuseumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Museum> _museumRepository;

        public MuseumService(IUnitOfWork unitOfWork, IImageService imageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
            _museumRepository = unitOfWork.GetRepository<Museum>();
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
    }
}