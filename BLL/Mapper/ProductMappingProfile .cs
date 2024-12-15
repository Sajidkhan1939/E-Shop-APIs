using AutoMapper;
using EShop.BLL.DTOS.ProductDTOs;
using EShop.BLL.Model.ProductModel;

namespace EShop.BLL.Mapper
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                 .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName))
                 .ForMember(dest => dest.ProductColor, opt => opt.MapFrom(src => src.ProductColor.ToString()))
                 .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.FileName).ToList()));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
