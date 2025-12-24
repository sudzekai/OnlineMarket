using AutoMapper;
using BLL.Services.Service;
using BLL.Types.Exceptions;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Orders;
using DAL.Efcore.Repositories.UOW;
using DTO.CompositeModels.OrderProducts;
using DTO.CompositeModels.Orders;
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

        public async Task<OrderFullInfoDto> GetFullInfoByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id)
                ?? throw new RecordNotFoundException($"Заказ с ID {id} не найден");

            var client = await _uow.Clients.GetByIdAsync(order.ClientId)
                ?? throw new RecordNotFoundException($"Клиент с ID {order.ClientId} не найден");

            var orderProducts = await _uow.OrderProducts.GetAllByOrderIdAsync(order.Id);

            OrderFullInfoDto result = new();

            result.Order = _mapper.Map<OrderFullDto>(order);
            result.ClientFullName = client.FullName;
            result.Products = [];

            foreach (var orderProduct in orderProducts)
            {
                OrderProductFullInfo product = new();

                var foundProduct = await _uow.Products.GetByArticleAsync(orderProduct.ProductArticle);
                _mapper.Map(foundProduct, product.Product);

                product.Amount = orderProduct.Amount;
                product.TotalPrice = orderProduct.Amount * foundProduct.Price;
                product.DiscountedPrice = orderProduct.Amount * (foundProduct.Price * (1 - product.Product.Discount / 100m));
                result.Products.Add(product);
                result.TotalDiscountedPrice += product.DiscountedPrice;
                result.TotalPrice += product.TotalPrice;
            }

            return result;
        }
    }
}
