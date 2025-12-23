using BLL.Services.Service;
using DTO.Models.Orders;

namespace BLL.Services.Orders
{
    public interface IOrdersService : IService<OrderFullDto, OrderCreateDto, OrderUpdateDto>
    {
    }
}