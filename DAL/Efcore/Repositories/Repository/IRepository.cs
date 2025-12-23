namespace DAL.Efcore.Repositories.Repository
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<TModel> AddAsync(TModel entity);
        Task<bool> DeleteAsync(int id);
        Task<List<TModel>> GetAllAsync();
        Task<List<TModel>> GetAllPagedAsync(int page, int pageSize);
        Task<TModel?> GetByIdAsync(int id);
    }
}