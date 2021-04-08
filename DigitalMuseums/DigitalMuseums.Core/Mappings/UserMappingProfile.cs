using AutoMapper;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Auth;

namespace DigitalMuseums.Core.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, BasePredefinedEntity>()
                .ForMember(
                    dest => dest.Name,
                    cfg => cfg.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}
