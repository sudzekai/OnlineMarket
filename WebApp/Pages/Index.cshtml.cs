using DTO.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebClient.WebClients.Products;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<ProductFullDto> Products { get; private set; } = new();

        private readonly IProductsWebClient _productsWebClient;

        public IndexModel(IProductsWebClient productsWebClient)
        {
            _productsWebClient = productsWebClient;
        }

        public async Task OnGet()
        {
            Products = await _productsWebClient.GetAllAsync("");
        }
    }
}
