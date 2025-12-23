using DAL.efcore.Repositories;
using DAL.Efcore.Data;
using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.Suppliers
{
    public class SuppliersRepository : Repository<Supplier>, ISuppliersRepository
    {
        public SuppliersRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
