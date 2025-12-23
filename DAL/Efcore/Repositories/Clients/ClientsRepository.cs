using DAL.Efcore.Data;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Efcore.Repositories.Clients
{
    public class ClientsRepository : Repository<Client>, IClientsRepository
    {
        public ClientsRepository(FinalProjectDbContext context) : base(context)
        {
        }

        public async Task<Client> GetByLoginAsync(string login)
            => await _dbSet.FirstOrDefaultAsync(c => c.Login.Equals(login));

        public async Task<Client> GetByLoginAndPasswordAsync(string login, string password)
            => await _dbSet.FirstOrDefaultAsync(c => c.Login.Equals(login) && c.Password.Equals(password));
    }
}
