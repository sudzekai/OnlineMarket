using DAL.Efcore.Data;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;

namespace DAL.Efcore.Repositories.Clients
{
    public class ClientsRepository : Repository<Client>, IClientsRepository
    {
        public ClientsRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
