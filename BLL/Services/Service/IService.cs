
namespace BLL.Services.Service
{
    public interface IService<TFullDto, TCreateDto, TUpdateDto>
        where TFullDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<TFullDto> AddAsync(TCreateDto createDto);
        Task<bool> DeleteAsync(int id);
        Task<List<TFullDto>> GetAllAsync();
        Task<List<TFullDto>> GetAllPagedAsync(int page, int pageSize);
        Task<TFullDto> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, TUpdateDto updateDto);
    }
}