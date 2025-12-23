using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;

namespace DAL.Efcore.Repositories.Orders
{
    public interface IOrdersRepository : IRepository<Order>
    {
    }
}