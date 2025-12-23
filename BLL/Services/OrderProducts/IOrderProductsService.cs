using DTO.Models.OrderProducts;

namespace BLL.Services.OrderProducts
{
    public interface IOrderProductsService
    {
        Task<OrderProductDto> AddAsync(OrderProductDto createDto);
        Task<bool> DeleteAsync(int orderId, string article);
        Task<List<OrderProductDto>> GetAllAsync();
        Task<List<OrderProductDto>> GetAllPagedAsync(int page, int pageSize);
        Task<OrderProductDto?> GetByPKAsync(int orderId, string article);
        Task<bool> UpdateAsync(int orderId, string article, OrderProductUpdateDto updateDto);
    }
}