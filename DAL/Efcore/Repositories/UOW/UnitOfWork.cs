using DAL.Efcore.Data;
using DAL.Efcore.Repositories.Categories;
using DAL.Efcore.Repositories.Clients;
using DAL.Efcore.Repositories.OrderProducts;
using DAL.Efcore.Repositories.Orders;
using DAL.Efcore.Repositories.Producers;
using DAL.Efcore.Repositories.Products;
using DAL.Efcore.Repositories.Suppliers;

namespace DAL.Efcore.Repositories.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinalProjectDbContext _context;

        public ICategoriesRepository Categories { get; }
        public IClientsRepository Clients { get; }
        public IOrderProductsRepository OrderProducts { get; }
        public IOrdersRepository Orders { get; }
        public IProducersRepository Producers { get; }
        public IProductsRepository Products { get; }
        public ISuppliersRepository Suppliers { get; }

        public UnitOfWork(FinalProjectDbContext context)
        {
            _context = context;

            Categories = new CategoriesRepository(context);
            Clients = new ClientsRepository(context);
            OrderProducts = new OrderProductsRepository(context);
            Orders = new OrdersRepository(context);
            Producers = new ProducersRepository(context);
            Products = new ProductsRepository(context);
            Suppliers = new SuppliersRepository(context);
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
