using DAL.efcore.Repositories;
using DAL.Efcore.Data;
using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.Producers
{
    public class ProducersRepository : Repository<Producer>, IProducersRepository
    {
        public ProducersRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
