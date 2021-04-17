using AutoMapper;
using DigitalMuseums.Core.Domain.DTO.Exhibit;
using DigitalMuseums.Core.Domain.DTO.Museum;
using DigitalMuseums.Core.Domain.DTO.Souvenir;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Mappings
{
    public class ImageMappingProfile : Profile
    {
        public ImageMappingProfile()
        {
            CreateMap<MuseumImagesUnit, Image>();
            CreateMap<SouvenirImagesUnit, Image>();

            CreateMap<ExhibitImagesUnit, Image>();
        }
    }
}