using AutoMapper;
using DAL.Efcore.Models;
using DTO.Models.OrderProducts;

namespace BLL.Mappers
{
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProduct, OrderProductDto>().ReverseMap();
            CreateMap<OrderProduct, OrderProductUpdateDto>().ReverseMap();
        }
    }
}
