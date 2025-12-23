using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.Products
{
    public interface IProductsRepository
    {
        Task<Product> AddAsync(Product entity);
        Task<bool> DeleteAsync(string article);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllPagedAsync(int page, int pageSize = 5);
        Task<Product?> GetByArticleAsync(string article);
    }
}