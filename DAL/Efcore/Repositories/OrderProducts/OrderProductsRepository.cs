using DAL.Efcore.Data;
using DAL.Efcore.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Efcore.Repositories.OrderProducts
{
    public class OrderProductsRepository : IOrderProductsRepository
    {
        private readonly DbSet<OrderProduct> _dbSet;

        public OrderProductsRepository(FinalProjectDbContext context)
        {
            _dbSet = context.Set<OrderProduct>();
        }

        public async Task<OrderProduct> AddAsync(OrderProduct entity)
            => (await _dbSet.AddAsync(entity)).Entity;

        public async Task<bool> DeleteAsync(int orderId, string article)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductArticle == article);

            if (entity is null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }

        public async Task<List<OrderProduct>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<List<OrderProduct>> GetAllPagedAsync(int page, int pageSize = 5)
        {
            if (page < 1 || pageSize < 1) return null;

            var result = _dbSet.Skip((page - 1) * pageSize)
                               .Take(pageSize);

            return await result.ToListAsync();
        }

        public async Task<OrderProduct?> GetByPKAsync(int orderId, string article)
            => await _dbSet.FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductArticle == article);

        public async Task<List<OrderProduct>> GetAllByOrderIdAsync(int orderId)
            => await _dbSet.Where(op => op.OrderId == orderId).ToListAsync();
    }
}
