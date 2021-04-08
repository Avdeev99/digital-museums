using DigitalMuseums.Core.Domain.DTO.Image;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IImageService
    {
        void AddAndUpload(BaseImagesUnit images);
    }
}