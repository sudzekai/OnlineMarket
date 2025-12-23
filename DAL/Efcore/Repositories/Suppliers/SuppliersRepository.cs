using DAL.Efcore.Data;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;

namespace DAL.Efcore.Repositories.Suppliers
{
    public class SuppliersRepository : Repository<Supplier>, ISuppliersRepository
    {
        public SuppliersRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
