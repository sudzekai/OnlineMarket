using DTO.Models.Categories;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Categories
{
    public interface ICategoriesWebClient : IWebClientBase<CategoryFullDto, CategoryActionDto, CategoryActionDto>
    {
    }
}