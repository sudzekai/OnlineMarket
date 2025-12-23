using DAL.Efcore.Data;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;

namespace DAL.Efcore.Repositories.Orders
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
