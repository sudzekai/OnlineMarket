using DTO.Models.Products;

namespace BLL.Services.Products
{
    public interface IProductsService
    {
        Task<ProductFullDto> AddAsync(ProductCreateDto createDto);
        Task<bool> DeleteAsync(string article);
        Task<List<ProductFullDto>> GetAllAsync();
        Task<List<ProductFullDto>> GetAllPagedAsync(int page, int pageSize);
        Task<ProductFullDto?> GetByArticleAsync(string article);
        Task<bool> UpdateAsync(int orderId, string article, ProductUpdateDto updateDto);
    }
}