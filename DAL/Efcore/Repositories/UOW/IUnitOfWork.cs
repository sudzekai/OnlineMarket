using DAL.Efcore.Repositories.Categories;
using DAL.Efcore.Repositories.Clients;
using DAL.Efcore.Repositories.OrderProducts;
using DAL.Efcore.Repositories.Orders;
using DAL.Efcore.Repositories.Producers;
using DAL.Efcore.Repositories.Products;
using DAL.Efcore.Repositories.Suppliers;

namespace DAL.Efcore.Repositories.UOW
{
    public interface IUnitOfWork
    {
        ICategoriesRepository Categories { get; }
        IClientsRepository Clients { get; }
        IOrderProductsRepository OrderProducts { get; }
        IOrdersRepository Orders { get; }
        IProducersRepository Producers { get; }
        IProductsRepository Products { get; }
        ISuppliersRepository Suppliers { get; }

        Task<int> SaveChangesAsync();
    }
}