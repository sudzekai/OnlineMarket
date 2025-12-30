using DTO.Models.Products;

namespace WebClient.WebClients.Products
{
    public interface IProductsWebClient
    {
        Task<bool> DeleteAsync(string token, string article);
        Task<List<ProductFullDto>> GetAllAsync(string token, int? page = null, int? pageSize = null);
        Task<ProductFullDto> GetByArticleAsync(string token, string article);
        Task<ProductFullDto> PostAsync(string token, ProductCreateDto createDto);
        Task<bool> PutAsync(string token, string article, ProductUpdateDto updateDto);
    }
}