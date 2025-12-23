using AutoMapper;
using DAL.Efcore.Models;
using DTO.Models.Orders;

namespace BLL.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderFullDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();
            CreateMap<Order, OrderUpdateStatusDto>().ReverseMap();
        }
    }
}
