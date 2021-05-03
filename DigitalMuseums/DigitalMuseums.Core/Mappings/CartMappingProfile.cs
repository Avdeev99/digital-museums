using AutoMapper;
using DigitalMuseums.Core.Domain.DTO.Cart;
using DigitalMuseums.Core.Domain.Models.Adjacent;
using DigitalMuseums.Core.Domain.Models.Order;

namespace DigitalMuseums.Core.Mappings
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<SouvenirOrderDetail, CurrentCartDetail>()
                .ForMember(
                    x => x.Souvenir,
                    opt => opt.MapFrom(x => x.Souvenir));
            CreateMap<Order, CurrentCart>()
                .ForMember(
                    x => x.OrderDetails,
                    opt => opt.MapFrom(x => x.OrderDetails));
        }
    }
}