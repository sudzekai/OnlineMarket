using DAL.efcore.Repositories;
using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.Orders
{
    public interface IOrdersRepository : IRepository<Order>
    {
    }
}