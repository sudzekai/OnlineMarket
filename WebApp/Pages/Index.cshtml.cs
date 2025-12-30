using DTO.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebClient.WebClients.Categories;
using WebClient.WebClients.Orders;
using WebClient.WebClients.Producers;
using WebClient.WebClients.Products;
using WebClient.WebClients.Suppliers;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductsWebClient _productsWebClient;
        private readonly ICategoriesWebClient _categoriesWebClient;
        private readonly IProducersWebClient _producersWebClient;
        private readonly ISuppliersWebClient _suppliersWebClient;
        private readonly IOrdersWebClient _ordersWebClient;

        public List<ProductFullDto> Products { get; private set; } = new();

        // Сортировка
        [BindProperty(SupportsGet = true)]
        public int? SelectedSortVariable { get; set; }

        // Фильтры

        [BindProperty(SupportsGet = true)]
        public string? PriceMin { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? PriceMax { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchBarText { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? FilterCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? FilterSupplier { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? FilterProducer { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool OnlyWithDiscount { get; set; } = false;


        public IndexModel(IProductsWebClient productsWebClient, ICategoriesWebClient categoriesWebClient, IProducersWebClient producersWebClient, IOrdersWebClient ordersWebClient, ISuppliersWebClient suppliersWebClient)
        {
            _productsWebClient = productsWebClient;
            _categoriesWebClient = categoriesWebClient;
            _producersWebClient = producersWebClient;
            _ordersWebClient = ordersWebClient;
            _suppliersWebClient = suppliersWebClient;
        }

        public async Task OnGet()
        {
            var productsList = await _productsWebClient.GetAllAsync("");
            var products = productsList.AsQueryable();

            var discountedPrices = products.Select(p => Math.Round(p.Price * ((100m - p.Discount) / 100m), 2)).ToList();

            PriceMax ??= discountedPrices.Max().ToString();
            PriceMin ??= discountedPrices.Min().ToString();

            var categories = await _categoriesWebClient.GetAllAsync("");
            var suppliers = await _suppliersWebClient.GetAllAsync("");
            var producers = await _producersWebClient.GetAllAsync("");

            var sortVariables = new Dictionary<int, string>
            {
                { 1, "Сначала дорогие" },
                { 2, "Сначала дешёвые" }
            };

            ViewData["Categories"] ??= new SelectList(categories, "Id", "Name");
            ViewData["Suppliers"] ??= new SelectList(suppliers, "Id", "Name");
            ViewData["Producers"] ??= new SelectList(producers, "Id", "Name");
            ViewData["SortVariables"] ??= new SelectList(sortVariables, "Key", "Value");

            if (!string.IsNullOrWhiteSpace(SearchBarText))
            {
                products = products.Where(p => p.Name.Contains(SearchBarText, StringComparison.OrdinalIgnoreCase) 
                                            || p.Description.Contains(SearchBarText, StringComparison.OrdinalIgnoreCase)
                                            || p.Article.Contains(SearchBarText, StringComparison.OrdinalIgnoreCase));
            }

            if (OnlyWithDiscount)
                products = products.Where(p => !p.Discount.Equals(0));

            if (decimal.TryParse(PriceMin.Replace(".", ","), out var priceMin) && decimal.TryParse(PriceMax.Replace(".", ","), out var priceMax))
            {
                if (priceMin <= priceMax)
                    products = products.Where(p => (p.Price * ((100m - p.Discount) / 100m)) >= priceMin
                                                && (p.Price * ((100m - p.Discount) / 100m)) <= priceMax);
                else
                    products = products.Where(p => (p.Price * ((100m - p.Discount) / 100m)) >= priceMin);
            }
            else
            {
                ModelState.Remove(nameof(PriceMax));
                ModelState.Remove(nameof(PriceMin));
                PriceMax = discountedPrices.Max().ToString();
                PriceMin = discountedPrices.Min().ToString();
            }

            if (FilterCategory > 0)
                products = products.Where(p => p.CategoryId.Equals(FilterCategory));

            if (FilterProducer > 0)
                products = products.Where(p => p.ProducerId.Equals(FilterProducer));

            if (FilterSupplier > 0)
                products = products.Where(p => p.SupplierId.Equals(FilterSupplier));

            products = SelectedSortVariable switch
            {
                1 => products.OrderByDescending(p => p.Price * ((100m - p.Discount) / 100m)),
                2 => products.OrderBy(p => p.Price * ((100m - p.Discount) / 100m)),
                _ => products.OrderByDescending(p => p.ImageName)
            };


            Products = [.. products];
        }
    }
}
