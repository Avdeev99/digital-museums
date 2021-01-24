using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Secondary;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace DigitalMuseums.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Image> _imageRepository;
        
        private readonly Cloudinary _cloudinary;
        
        public ImageService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<CloudinaryOptions> cloudinaryConfig)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageRepository = unitOfWork.GetRepository<Image>();
            
            var account = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }
        
        public void AddAndUpload(BaseImagesUnit imageData)
        {
            var imageItem = _mapper.Map<Image>(imageData);

            foreach (var image in imageData.ImagesData)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.Stream)
                };
                
                var uploadResult = _cloudinary.Upload(uploadParams);
                imageItem.Path = uploadResult.Uri.ToString();

                _imageRepository.Create(imageItem);
            }
            
            _unitOfWork.SaveChanges();
        }
    }
}