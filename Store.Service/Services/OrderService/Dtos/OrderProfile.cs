using AutoMapper;
using Store.Data.Entities.IdentityEntities;
using Store.Data.Entities.OrderEntities;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, ShippingAddress>().ReverseMap();

            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.DeliveryMethodName, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.orderedItem.ProductItemId))
                .ForMember(dest => dest.ProductItemName, options => options.MapFrom(src => src.orderedItem.ProductItemName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.orderedItem.PictureUrl))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemsUrlResolver>()).ReverseMap();

        }
    }
}
