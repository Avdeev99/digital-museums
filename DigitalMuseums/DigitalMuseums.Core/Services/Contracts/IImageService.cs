using DigitalMuseums.Core.Domain.DTO;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IImageService
    {
        void AddAndUpload(BaseImagesUnit images);
    }
}