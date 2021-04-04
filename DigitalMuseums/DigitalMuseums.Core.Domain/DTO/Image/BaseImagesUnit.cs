using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Image
{
    public abstract class BaseImagesUnit
    {
        public List<ImageData> ImagesData { get; set; }
    }
}