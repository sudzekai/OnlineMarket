using DTO.Models.Categories;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Categories
{
    public class CategoriesWebClient : WebClientBase<CategoryFullDto, CategoryActionDto, CategoryActionDto>, ICategoriesWebClient
    {
        public CategoriesWebClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
