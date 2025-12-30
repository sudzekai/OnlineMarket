using DTO.Models.OrderProducts;
using DTO.Models.Orders;

namespace WebClient.WebClients.OrderProducts
{
    public interface IOrderProductsWebClient
    {
        Task<bool> DeleteAsync(string token, int orderId, string article);
        Task<List<OrderProductDto>> GetAllAsync(string token, int? page = null, int? pageSize = null);
        Task<OrderProductDto> GetByOrderIdAndArticleAsync(string token, int orderId, string article);
        Task<OrderProductDto> PostAsync(string token, OrderProductDto createDto);
        Task<bool> PutAsync(string token, int orderId, string article, OrderUpdateDto updateDto);
    }
}