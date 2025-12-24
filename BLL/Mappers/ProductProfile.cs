using AutoMapper;
using DAL.Efcore.Models;
using DTO.Models.Products;
using System.Runtime.CompilerServices;

namespace BLL.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductFullDto>().ReverseMap();
            CreateMap<Product, ProductSimpleDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }
}
