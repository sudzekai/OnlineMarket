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
    /// <summary>
    /// Сервис для управления заказами.
    /// </summary>
    /// <remarks>
    /// Предоставляет стандартные CRUD-операции и расширенный метод для получения полной 
    /// агрегированной информации о заказе, включая данные клиента, список товаров и финансовые итоги.
    /// </remarks>
    public class OrdersService : Service<Order, OrderFullDto, OrderCreateDto, OrderUpdateDto>, IOrdersService
    {
        private readonly new IOrdersRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrdersService"/>.
        /// </summary>
        /// <param name="uow">Единица работы для доступа к репозиториям клиентов, продуктов и позиций заказа.</param>
        /// <param name="repository">Репозиторий заказов.</param>
        /// <param name="mapper">Экземпляр AutoMapper для преобразования сущностей в DTO.</param>
        public OrdersService(IUnitOfWork uow, IOrdersRepository repository, IMapper mapper) : base(uow, repository, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Формирует детализированную информацию о заказе с расчетом стоимостей.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Объект <see cref="OrderFullInfoDto"/>, содержащий данные заказа, клиента и список всех позиций с учетом скидок.</returns>
        /// <exception cref="RecordNotFoundException">
        /// Выбрасывается, если не найден сам заказ или связанный с ним клиент.
        /// </exception>
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