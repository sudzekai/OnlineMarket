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
    }
}
