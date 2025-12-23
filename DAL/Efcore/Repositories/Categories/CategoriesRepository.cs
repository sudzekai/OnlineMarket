using DAL.Efcore.Data;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;

namespace DAL.Efcore.Repositories.Categories
{
    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(FinalProjectDbContext context) : base(context)
        {
        }
    }
}
