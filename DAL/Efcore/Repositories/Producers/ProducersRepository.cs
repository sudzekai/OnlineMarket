using DAL.Efcore.Data;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;

namespace DAL.Efcore.Repositories.Producers
{
    public class ProducersRepository : Repository<Producer>, IProducersRepository
    {
        public ProducersRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
