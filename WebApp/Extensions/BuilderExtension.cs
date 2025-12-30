using WebClient.WebClients.Categories;
using WebClient.WebClients.Clients;
using WebClient.WebClients.OrderProducts;
using WebClient.WebClients.Orders;
using WebClient.WebClients.Producers;
using WebClient.WebClients.Products;
using WebClient.WebClients.Suppliers;

namespace WebApp.Extensions
{
    internal static partial class BuilderExtension
    {
        public static void AddClients(this WebApplicationBuilder builder)
        {
            string baseUrl = builder.Configuration["BaseUrl"] ?? throw new ArgumentNullException("BaseUrl не найден в файле конфигурации");

            builder.Services.AddHttpClient<ICategoriesWebClient, CategoriesWebClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "Categories");
            });

            builder.Services.AddHttpClient<IClientsWebClient, ClientsWebClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "Clients");
            });

            builder.Services.AddHttpClient<IOrderProductsWebClient, OrderProductsWebClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "OrderProducts");
            });

            builder.Services.AddHttpClient<IOrdersWebClient, OrdersWebClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "Orders");
            });

            builder.Services.AddHttpClient<IProducersWebClient, ProducersWebClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "Producers");
            });

            builder.Services.AddHttpClient<IProductsWebClient, ProductsWebClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "Products");
            });

            builder.Services.AddHttpClient<ISuppliersWebClient, SuppliersWebClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "Suppliers");
            });
        }
    }
}
