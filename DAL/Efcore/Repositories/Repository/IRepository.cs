namespace DAL.Efcore.Repositories.Repository
{
    public interface IRepository<TModel> where TModel : class
    {
        Task AddAsync(TModel entity);
        Task<bool> DeleteAsync(int id);
        Task<List<TModel>> GetAllAsync();
        Task<List<TModel>> GetAllPagedAsync(int page, int pageSize = 5);
        Task<TModel?> GetByIdAsync(int id);
    }
}