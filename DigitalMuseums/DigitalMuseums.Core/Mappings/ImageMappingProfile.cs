using AutoMapper;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Mappings
{
    public class ImageMappingProfile : Profile
    {
        public ImageMappingProfile()
        {
            CreateMap<MuseumImagesUnit, Image>();
        }
    }
}