
namespace WebClient.WebClients.WebClient
{
    public interface IWebClientBase<TFullDto, TCreateDto, TUpdateDto>
        where TFullDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<bool> DeleteAsync(string token, int id);
        Task<List<TFullDto>> GetAllAsync(string token, int? page = null, int? pageSize = null);
        Task<TFullDto> GetByIdAsync(string token, int id);
        Task<TFullDto> PostAsync(string token, TCreateDto createDto);
        Task<bool> PutAsync(string token, int id, TUpdateDto updateDto);
    }
}