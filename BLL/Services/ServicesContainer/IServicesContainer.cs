using BLL.Services.Categories;
using BLL.Services.Clients;
using BLL.Services.OrderProducts;
using BLL.Services.Orders;
using BLL.Services.Producers;
using BLL.Services.Products;
using BLL.Services.Suppliers;

namespace BLL.Services.ServicesContainer
{
    public interface IServicesContainer
    {
        ICategoriesService Categories { get; }
        IClientsService Clients { get; }
        IOrderProductsService OrderProducts { get; }
        IOrdersService Orders { get; }
        IProducersService Producers { get; }
        IProductsService Products { get; }
        ISuppliersService Suppliers { get; }
    }
}