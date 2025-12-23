using DAL.efcore.Repositories;
using DAL.Efcore.Data;
using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.Clients
{
    public class ClientsRepository : Repository<Client>, IClientsRepository
    {
        public ClientsRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
