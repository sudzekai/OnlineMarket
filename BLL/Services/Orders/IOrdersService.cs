using BLL.Services.Service;
using DTO.CompositeModels.Orders;
using DTO.Models.Orders;

namespace BLL.Services.Orders
{
    public interface IOrdersService : IService<OrderFullDto, OrderCreateDto, OrderUpdateDto>
    {
        Task<OrderFullInfoDto> GetFullInfoByIdAsync(int id);
    }
}