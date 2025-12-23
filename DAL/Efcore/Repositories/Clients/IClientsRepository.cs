using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;

namespace DAL.Efcore.Repositories.Clients
{
    public interface IClientsRepository : IRepository<Client>
    {
    }
}