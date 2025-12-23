using DAL.efcore.Repositories;
using DAL.Efcore.Data;
using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.Orders
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
