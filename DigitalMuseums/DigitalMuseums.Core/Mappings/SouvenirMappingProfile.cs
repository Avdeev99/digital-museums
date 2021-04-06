using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DigitalMuseums.Core.Domain.DTO.Image;
using DigitalMuseums.Core.Domain.DTO.Souvenir;
using DigitalMuseums.Core.Domain.Models.Domain;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Mappings
{
    public class SouvenirMappingProfile : Profile
    {
        public SouvenirMappingProfile()
        {
            CreateMap<List<IFormFile>, SouvenirImagesUnit>().ConvertUsing((source, dest) =>
            {
                if (source == null)
                {
                    return null;
                }
                
                var result = new SouvenirImagesUnit{ImagesData = new List<ImageData>()};
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
            
            CreateMap<Souvenir, SouvenirItem>()
                .ForMember(dest => dest.ImagePaths, opt => opt.MapFrom(s => s.Images.Select(i => i.Path)));
        }
    }
}