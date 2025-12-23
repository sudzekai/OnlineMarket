using DAL.efcore.Repositories;
using DAL.Efcore.Data;
using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.OrderProducts
{
    public class OrderProductsRepository : Repository<OrderProduct>, IOrderProductsRepository
    {
        public OrderProductsRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
