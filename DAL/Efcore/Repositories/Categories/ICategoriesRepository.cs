using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Repository;

namespace DAL.Efcore.Repositories.Categories
{
    public interface ICategoriesRepository : IRepository<Category>
    {
    }
}