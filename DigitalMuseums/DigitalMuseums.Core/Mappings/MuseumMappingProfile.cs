using System.Collections.Generic;
using System.IO;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Museum;
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
        }
    }
}