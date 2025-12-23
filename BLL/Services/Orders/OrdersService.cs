using AutoMapper;
using BLL.Services.Service;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Orders;
using DAL.Efcore.Repositories.Repository;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.Orders;

namespace BLL.Services.Orders
{
    public class OrdersService : Service<Order, OrderFullDto, OrderCreateDto, OrderUpdateDto>, IOrdersService
    {
        private readonly new IOrdersRepository _repository;
        public OrdersService(IUnitOfWork uow, IOrdersRepository repository, IMapper mapper) : base(uow, repository, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
