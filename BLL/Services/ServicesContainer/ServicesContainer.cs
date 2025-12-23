using AutoMapper;
using BLL.Services.Categories;
using BLL.Services.Clients;
using BLL.Services.OrderProducts;
using BLL.Services.Orders;
using BLL.Services.Producers;
using BLL.Services.Products;
using BLL.Services.Suppliers;
using DAL.Efcore.Repositories.UOW;

namespace BLL.Services.ServicesContainer
{
    public class ServicesContainer : IServicesContainer
    {
        public ICategoriesService Categories { get; }
        public IClientsService Clients { get; }
        public IOrderProductsService OrderProducts { get; }
        public IOrdersService Orders { get; }
        public IProducersService Producers { get; }
        public IProductsService Products { get; }
        public ISuppliersService Suppliers { get; }

        public ServicesContainer(IUnitOfWork uow, IMapper mapper)
        {
            Categories = new CategoriesService(uow, uow.Categories, mapper);
            Clients = new ClientsService(uow, uow.Clients, mapper);
            OrderProducts = new OrderProductsService(uow, uow.OrderProducts, mapper);
            Orders = new OrdersService(uow, uow.Orders, mapper);
            Producers = new ProducersService(uow, uow.Producers, mapper);
            Products = new ProductsService(uow, uow.Products, mapper);
            Suppliers = new SuppliersService(uow, uow.Suppliers, mapper);
        }
    }
}
