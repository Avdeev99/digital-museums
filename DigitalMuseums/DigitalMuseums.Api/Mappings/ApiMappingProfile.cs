using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Exhibit;
using DigitalMuseums.Api.Contracts.Requests.Genre;
using DigitalMuseums.Api.Contracts.Requests.Museum;
using DigitalMuseums.Api.Contracts.Requests.Souvenir;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Secondary;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Api.Contracts.Responses.Exhibit;
using DigitalMuseums.Api.Contracts.Responses.Museum;
using DigitalMuseums.Api.Contracts.Responses.Souvenir;
using DigitalMuseums.Api.Contracts.ViewModels;
using DigitalMuseums.Core.Domain.DTO.Exhibit;
using DigitalMuseums.Core.Domain.DTO.Museum;
using DigitalMuseums.Core.Domain.DTO.Souvenir;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Auth;
using Google.Apis.Auth;

namespace DigitalMuseums.Api.Mappings
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<BasePredefinedEntity, BasePredefinedEntityResponse>().ReverseMap();

            CreateMap<AddGenreRequest, Genre>();
           
            CreateMap<LinkUserToMuseumRequest, LinkUserToMuseumDto>();
            CreateMap<FilterMuseumsRequest, FilterMuseumsDto>();
            CreateMap<CreateMuseumRequest, CreateMuseumDto>()
                .ForMember(dest => dest.ImagesData, opt => opt.MapFrom(s => s.Images));
            CreateMap<UpdateMuseumRequest, UpdateMuseumDto>()
                .ForMember(dest => dest.ImagesData, opt => opt.MapFrom(s => s.Images));


            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            
            CreateMap<AuthDto, AuthResponse>().ReverseMap();
            
            CreateMap<GoogleJsonWebSignature.Payload, User>()
                .ForMember(dest => dest.UserName, cfg => cfg.MapFrom(src => src.Name));
            
            CreateMap<FilteredMuseumItem, GetFilteredMuseumsResponseItem>();

            CreateMap<MuseumItem, GetMuseumResponse>()
                .ForMember(
                    dest => dest.Country,
                    cfg => 
                        cfg.MapFrom((src, dest) => src.Location?.City?.Region?.Country))
                .ForMember(
                    dest => dest.Region,
                    cfg => cfg.MapFrom((src, dest) => src.Location?.City?.Region))
                .ForMember(
                    dest => dest.City,
                    cfg => cfg.MapFrom((src, dest) => src.Location?.City))
                .ForMember(
                    dest => dest.Address,
                    cfg => cfg.MapFrom((src, dest) => src.Location?.Address));

            CreateMap<FilterExhibitsRequest, FilterExhibitsDto>().ReverseMap();
            CreateMap<FilteredExhibitItem, GetFilteredExhibitsResponseItem>();
            CreateMap<CreateExhibitRequest, CreateExhibitDto>()
                .ForMember(dest => dest.ImagesData, opt => opt.MapFrom(s => s.Images));
            CreateMap<UpdateExhibitRequest, UpdateExhibitDto>()
                .ForMember(dest => dest.ImagesData, opt => opt.MapFrom(s => s.Images));
            
            CreateMap<CreateSouvenirRequest, CreateSouvenirDto>()
                .ForMember(dest => dest.ImagesData, opt => opt.MapFrom(s => s.Images));
            CreateMap<SouvenirItem, GetSouvenirResponse>();
            CreateMap<UpdateSouvenirRequest, UpdateSouvenirDto>()
                .ForMember(dest => dest.ImagesData, opt => opt.MapFrom(s => s.Images));
            CreateMap<FilterSouvenirsRequest, FilterSouvenirsDto>();
            CreateMap<FilteredSouvenirItem, GetFilteredSouvenirsResponseItem>();
        }
    }
}