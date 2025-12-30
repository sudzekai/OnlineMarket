using DTO.Models.Categories;
using DTO.Models.Clients;
using DTO.Models.OrderProducts;
using DTO.Models.Orders;
using DTO.Models.Producers;
using DTO.Models.Products;
using DTO.Models.Suppliers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebClient.WebClients.Categories;
using WebClient.WebClients.Clients;
using WebClient.WebClients.OrderProducts;
using WebClient.WebClients.Orders;
using WebClient.WebClients.Producers;
using WebClient.WebClients.Products;
using WebClient.WebClients.Suppliers;

namespace WebApp.Pages.Debug
{
    public class WebClientsTestsModel : PageModel
    {
        public WebClientsTestsModel(ICategoriesWebClient categoriesWebClient, 
            IClientsWebClient clientsWebClient, 
            IOrderProductsWebClient orderProductsWebClient, 
            IOrdersWebClient ordersWebClient, 
            IProducersWebClient producersWebClient, 
            ISuppliersWebClient suppliersWebClient,
            IProductsWebClient productsWebClient)
        {
            _productsWebClient = productsWebClient;
            _categoriesWebClient = categoriesWebClient;
            _clientsWebClient = clientsWebClient;
            _orderProductsWebClient = orderProductsWebClient;
            _ordersWebClient = ordersWebClient;
            _producersWebClient = producersWebClient;
            _suppliersWebClient = suppliersWebClient;
        }

        private ICategoriesWebClient _categoriesWebClient;

        private IClientsWebClient _clientsWebClient;

        private IOrderProductsWebClient _orderProductsWebClient;

        private IOrdersWebClient _ordersWebClient;

        private IProducersWebClient _producersWebClient;

        private IProductsWebClient _productsWebClient;

        private ISuppliersWebClient _suppliersWebClient;

        public List<CategoryFullDto> Categories { get; set; } = new();
        public List<ClientFullDto> Clients { get; set; } = new();
        public List<OrderProductDto> OrderProducts { get; set; } = new();
        public List<OrderFullDto> Orders { get; set; } = new();
        public List<ProducerFullDto> Producers { get; set; } = new();
        public List<ProductFullDto> Products { get; set; } = new();
        public List<SupplierFullDto> Suppliers { get; set; } = new();

        public async Task OnGet()
        {
            string dbgToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiOTRkNW91c0BnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiLQkNC00LzQuNC90LjRgdGC0YDQsNGC0L7RgCIsImV4cCI6MTc2NzE1NjQ0NiwiaXNzIjoiTXlBcHAiLCJhdWQiOiJNeUF1ZGllbmNlIn0.AllAjbm6Fx7J14DEfxkaSVr6LnRSViiJtCTVjp1E_JU";
            Categories = await _categoriesWebClient.GetAllAsync(dbgToken);
            Clients = await _clientsWebClient.GetAllAsync(dbgToken);
            OrderProducts = await _orderProductsWebClient.GetAllAsync(dbgToken);
            Products = await _productsWebClient.GetAllAsync(dbgToken);
            Orders = await _ordersWebClient.GetAllAsync(dbgToken);
            Producers = await _producersWebClient.GetAllAsync(dbgToken);
            Suppliers = await _suppliersWebClient.GetAllAsync(dbgToken);
        }
    }
}
