using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Categories;
using DAL.Efcore.Repositories.Clients;
using DAL.Efcore.Repositories.OrderProducts;
using DAL.Efcore.Repositories.Orders;
using DAL.Efcore.Repositories.Producers;
using DAL.Efcore.Repositories.Products;
using DAL.Efcore.Repositories.Repository;
using DAL.Efcore.Repositories.Suppliers;

namespace API.Extensions
{
    internal static partial class BuilderExtension
    {
        private static void AddRepositories(this WebApplicationBuilder builder)
        {
            List<Type> models = new List<Type>
            {
                typeof(Category),
                typeof(Client),
                typeof(Order),
                typeof(Producer),
                typeof(Supplier)
            };

            foreach (var model in models)
            {
                Type service = typeof(IRepository<>).MakeGenericType(model);
                Type implementation = typeof(Repository<>).MakeGenericType(model);

                builder.Services.AddScoped(service, implementation);
            }

            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
            builder.Services.AddScoped<IOrderProductsRepository, OrderProductsRepository>();
            builder.Services.AddScoped<IProducersRepository, ProducersRepository>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<ISuppliersRepository, SuppliersRepository>();
        }
    }
}
