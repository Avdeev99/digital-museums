using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Domain.Models.Location;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Mappings
{
    public class MuseumMappingProfile : Profile
    {
        public MuseumMappingProfile()
        {
            CreateMap<List<IFormFile>, MuseumImagesUnit>().ConvertUsing((source, dest) =>
            {
                if (source == null)
                {
                    return null;
                }
                
                var result = new MuseumImagesUnit{ImagesData = new List<ImageData>()};
                foreach (var file in source)
                {
                    result.ImagesData.Add(new ImageData
                    {
                        Stream = file.OpenReadStream(),
                        FileName = file.Name
                    });
                }

                return result;
            });

            CreateMap<MuseumDto, Museum>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(s => new Location
                {
                    CityId = s.CityId,
                    Address = s.Address
                }));

            CreateMap<Museum, FilteredMuseumItem>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(s => s.Genre.Name))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(s => s.Images.First().Path));
            
            CreateMap<Museum, MuseumItem>()
                .ForMember(dest => dest.ImagePaths, opt => opt.MapFrom(s => s.Images.Select(i => i.Path)));
        }
    }
}