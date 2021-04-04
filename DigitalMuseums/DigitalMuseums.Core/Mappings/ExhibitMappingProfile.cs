using System.Collections.Generic;
using AutoMapper;
using DigitalMuseums.Core.Domain.DTO.Exhibit;
using DigitalMuseums.Core.Domain.DTO.Image;
using DigitalMuseums.Core.Domain.Models.Domain;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Mappings
{
    public class ExhibitMappingProfile : Profile
    {
        public ExhibitMappingProfile()
        {
            CreateMap<List<IFormFile>, ExhibitImagesUnit>().ConvertUsing((source, dest) =>
            {
                if (source == null)
                {
                    return null;
                }

                var result = new ExhibitImagesUnit { ImagesData = new List<ImageData>() };
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

            CreateMap<CreateExhibitDto, Exhibit>().ReverseMap();

            CreateMap<Exhibit, FilteredExhibitItem>().ReverseMap();
        }
    }
}
