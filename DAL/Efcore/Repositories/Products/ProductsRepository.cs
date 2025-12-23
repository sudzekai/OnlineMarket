using DAL.Efcore.Data;
using DAL.Efcore.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Efcore.Repositories.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DbSet<Product> _dbSet;

        public ProductsRepository(FinalProjectDbContext context)
            => _dbSet = context.Set<Product>();

        public virtual async Task AddAsync(Product entity)
            => await _dbSet.AddAsync(entity);

        public virtual async Task<bool> DeleteAsync(string article)
        {
            var entity = await _dbSet.FindAsync(article);

            if (entity is null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }

        public virtual async Task<List<Product>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public virtual async Task<List<Product>> GetAllPagedAsync(int page, int pageSize = 5)
        {
            if (page < 1 || pageSize < 1) return null;

            var result = _dbSet.Skip((page - 1) * pageSize)
                               .Take(pageSize);

            return await result.ToListAsync();
        }

        public virtual async Task<Product?> GetByArticleAsync(string article)
            => await _dbSet.FindAsync(article);
    }
}
