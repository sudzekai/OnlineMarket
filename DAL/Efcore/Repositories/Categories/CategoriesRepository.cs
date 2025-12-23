using DAL.efcore.Repositories;
using DAL.Efcore.Data;
using DAL.Efcore.Models;

namespace DAL.Efcore.Repositories.Categories
{
    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
