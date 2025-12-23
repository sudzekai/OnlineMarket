using DAL.Efcore.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Efcore.Repositories.Repository
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly DbSet<TModel> _dbSet;

        public Repository(FinalProjectDbContext context)
            => _dbSet = context.Set<TModel>();

        public virtual async Task AddAsync(TModel entity)
            => await _dbSet.AddAsync(entity);

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }

        public virtual async Task<List<TModel>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public virtual async Task<List<TModel>> GetAllPagedAsync(int page, int pageSize = 5)
        {
            if (page < 1 || pageSize < 1) return null;

            var result = _dbSet.Skip((page - 1) * pageSize)
                               .Take(pageSize);

            return await result.ToListAsync();
        }

        public virtual async Task<TModel?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);
    }
}
