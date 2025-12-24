using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.OrderProducts
{
    public interface IOrderProductsRepository
    {
        Task<OrderProduct> AddAsync(OrderProduct entity);
        Task<bool> DeleteAsync(int orderId, string article);
        Task<List<OrderProduct>> GetAllAsync();
        Task<List<OrderProduct>> GetAllByOrderIdAsync(int orderId);
        Task<List<OrderProduct>> GetAllPagedAsync(int page, int pageSize = 5);
        Task<OrderProduct?> GetByPKAsync(int orderId, string article);
    }
}