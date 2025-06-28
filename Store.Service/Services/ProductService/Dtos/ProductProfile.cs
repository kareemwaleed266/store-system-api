using AutoMapper;
using Store.Data.Entities;

namespace Store.Service.Services.ProductService.Dtos
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Data.Entities.Product, ProductDetailsDto>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName,  options => options.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.PictureUrl,options => options.MapFrom<ProductUrlResolver>());
            CreateMap<ProductType, BrandTypeDetailsDto>();
            CreateMap<ProductBrand, BrandTypeDetailsDto>();
        }
    }
}
