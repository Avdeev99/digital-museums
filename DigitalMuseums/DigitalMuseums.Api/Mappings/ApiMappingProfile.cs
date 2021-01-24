using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Api.Contracts.ViewModels;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Auth;
using Google.Apis.Auth;

namespace DigitalMuseums.Api.Mappings
{
    /// <summary>
    /// Represents mapping profile.
    /// </summary>
    public class ApiMappingProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiMappingProfile"/> class.
        /// </summary>
        public ApiMappingProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            
            CreateMap<AuthDto, AuthResponse>().ReverseMap();

            CreateMap<GoogleJsonWebSignature.Payload, User>()
                .ForMember(dest => dest.UserName, cfg => cfg.MapFrom(src => src.Name));
        }
    }
}