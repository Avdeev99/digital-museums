using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Responses.Exhibition;
using DigitalMuseums.Core.Domain.DTO.Exhibition;
using DigitalMuseums.Core.Domain.DTO.Image;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Domain;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Mappings
{
    public class ExhibitionMappingProfile : Profile
    {
        public ExhibitionMappingProfile()
        {
            CreateMap<List<IFormFile>, ExhibitionImagesUnit>().ConvertUsing((source, dest) =>
            {
                if (source == null)
                {
                    return null;
                }

                var result = new ExhibitionImagesUnit { ImagesData = new List<ImageData>() };
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

            CreateMap<CreateExhibitionDto, Exhibition>().ReverseMap();

            CreateMap<Exhibition, FilteredExhibitionItem>().ReverseMap();

            CreateMap<Exhibition, ExhibitionItem>()
                .ForMember(
                    dest => dest.ImagePaths, 
                    opt => opt.MapFrom(s => s.Images.Select(i => i.Path)))
                .ReverseMap();

            CreateMap<ExhibitionItem, GetExhibitionResponse>().ReverseMap();

            CreateMap<Exhibition, BasePredefinedEntity>().ReverseMap();
        }
    }
}