using BLL.Services.Categories;
using BLL.Services.Clients;
using BLL.Services.OrderProducts;
using BLL.Services.Orders;
using BLL.Services.Producers;
using BLL.Services.Products;
using BLL.Services.Service;
using BLL.Services.Suppliers;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Categories;
using DAL.Efcore.Repositories.Clients;
using DAL.Efcore.Repositories.OrderProducts;
using DAL.Efcore.Repositories.Orders;
using DAL.Efcore.Repositories.Producers;
using DAL.Efcore.Repositories.Products;
using DAL.Efcore.Repositories.Repository;
using DAL.Efcore.Repositories.Suppliers;
using DTO.Models.Categories;
using DTO.Models.Clients;
using DTO.Models.Orders;
using DTO.Models.Producers;
using DTO.Models.Suppliers;

namespace API.Extensions
{
    internal static partial class BuilderExtension
    {
        private static void AddRepositories(this WebApplicationBuilder builder)
        {
            List<Type> models =
            [
                typeof(Category),
                typeof(Client),
                typeof(Order),
                typeof(Producer),
                typeof(Supplier)
            ];

            foreach (var model in models)
            {
                Type repository = typeof(IRepository<>).MakeGenericType(model);
                Type implementation = typeof(Repository<>).MakeGenericType(model);

                builder.Services.AddScoped(repository, implementation);
            }

            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
            builder.Services.AddScoped<IOrderProductsRepository, OrderProductsRepository>();
            builder.Services.AddScoped<IProducersRepository, ProducersRepository>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<ISuppliersRepository, SuppliersRepository>();
        }
        
        private static void AddServices(this WebApplicationBuilder builder)
        {
            List<(Type Model, Type FullDto, Type CreateDto, Type UpdateDto)> fullModels =
            [
                (typeof(Category), typeof(CategoryFullDto), typeof(CategoryActionDto), typeof(CategoryActionDto)),
                (typeof(Client), typeof(ClientFullDto), typeof(ClientCreateDto), typeof(ClientUpdateDto)),
                (typeof(Order), typeof(OrderFullDto), typeof(OrderCreateDto), typeof(OrderUpdateDto)),
                (typeof(Producer), typeof(ProducerFullDto), typeof(ProducerActionDto), typeof(ProducerActionDto)),
                (typeof(Supplier), typeof(SupplierFullDto), typeof(SupplierActionDto), typeof(SupplierActionDto))
            ];

            foreach (var fullModel in fullModels)
            {
                Type service = typeof(IService<,,>).MakeGenericType(fullModel.FullDto, fullModel.CreateDto, fullModel.UpdateDto);
                Type implementation = typeof(Service<,,,>).MakeGenericType(fullModel.Model, fullModel.FullDto, fullModel.CreateDto, fullModel.UpdateDto);

                builder.Services.AddScoped(service, implementation);
            }

            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<IClientsService, ClientsService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IOrderProductsService, OrderProductsService>();
            builder.Services.AddScoped<IProducersService, ProducersService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<ISuppliersService, SuppliersService>();
        }
    }
}
