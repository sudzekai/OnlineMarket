using DAL.efcore.Repositories;
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
    }
}
