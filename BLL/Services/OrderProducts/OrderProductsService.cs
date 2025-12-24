using AutoMapper;
using BLL.Types.Exceptions;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.OrderProducts;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.OrderProducts;

namespace BLL.Services.OrderProducts
{
    /// <summary>
    /// Сервис для управления позициями товаров в заказах.
    /// </summary>
    /// <remarks>
    /// Позволяет управлять связями между заказами и товарами, включая изменение количества 
    /// и получение списков товаров для конкретных заказов.
    /// </remarks>
    public class OrderProductsService : IOrderProductsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IOrderProductsRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrderProductsService"/>.
        /// </summary>
        /// <param name="uow">Единица работы.</param>
        /// <param name="repository">Репозиторий позиций заказа.</param>
        /// <param name="mapper">Экземпляр AutoMapper.</param>
        public OrderProductsService(IUnitOfWork uow, IOrderProductsRepository repository, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Возвращает все записи о позициях во всех заказах.
        /// </summary>
        /// <returns>Список всех позиций заказов.</returns>
        /// <exception cref="RecordNotFoundException">Если в базе нет ни одной записи.</exception>
        public async Task<List<OrderProductDto>> GetAllAsync()
        {
            var models = await _repository.GetAllAsync();

            if (models is null || models.Count == 0)
                throw new RecordNotFoundException("Записи объекта не найдены");

            return _mapper.Map<List<OrderProductDto>>(models);
        }

        /// <summary>
        /// Возвращает постраничный список всех позиций заказов.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <returns>Список позиций для текущей страницы.</returns>
        public async Task<List<OrderProductDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var models = await _repository.GetAllPagedAsync(page, pageSize);

            if (models is null || models.Count == 0)
                throw new RecordNotFoundException("Записи объекта не найдены");

            return _mapper.Map<List<OrderProductDto>>(models);
        }

        /// <summary>
        /// Находит конкретную позицию в заказе по составному ключу.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="article">Артикул товара.</param>
        /// <returns>DTO позиции заказа.</returns>
        /// <exception cref="RecordNotFoundException">Если позиция с таким ключом не найдена.</exception>
        public async Task<OrderProductDto?> GetByPKAsync(int orderId, string article)
        {
            var model = await _repository.GetByPKAsync(orderId, article)
                ?? throw new RecordNotFoundException($"Запись объекта PK {orderId}:{article} не найдена");

            return _mapper.Map<OrderProductDto>(model);
        }

        /// <summary>
        /// Добавляет товар в заказ.
        /// </summary>
        /// <param name="createDto">Данные новой позиции заказа.</param>
        /// <returns>Созданная позиция заказа.</returns>
        /// <exception cref="RecordCreationException">Если не удалось добавить запись в репозиторий.</exception>
        /// <exception cref="RecordSavingException">Если не удалось сохранить изменения в базе данных.</exception>
        public virtual async Task<OrderProductDto> AddAsync(OrderProductDto createDto)
        {
            var model = _mapper.Map<OrderProduct>(createDto);

            var createdModel = await _repository.AddAsync(model)
                ?? throw new RecordCreationException("Ошибка при создании записи объекта");

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД о создании объекта");

            return _mapper.Map<OrderProductDto>(createdModel);
        }

        /// <summary>
        /// Удаляет позицию из заказа.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="article">Артикул товара.</param>
        /// <returns><c>true</c>, если удаление успешно.</returns>
        /// <exception cref="RecordDeletionException">Если запись не найдена или не удалена.</exception>
        public virtual async Task<bool> DeleteAsync(int orderId, string article)
        {
            var isDeleted = await _repository.DeleteAsync(orderId, article);

            if (!isDeleted)
                throw new RecordDeletionException("Ошибка при удалении записи объекта");

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД об удалении объекта");

            return isDeleted;
        }

        /// <summary>
        /// Обновляет данные позиции (например, количество) в заказе.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="article">Артикул товара.</param>
        /// <param name="updateDto">Новые данные для обновления.</param>
        /// <returns><c>true</c>, если обновление успешно.</returns>
        /// <exception cref="RecordNotFoundException">Если позиция для обновления не найдена.</exception>
        public virtual async Task<bool> UpdateAsync(int orderId, string article, OrderProductUpdateDto updateDto)
        {
            var existingModel = await _repository.GetByPKAsync(orderId, article)
                ?? throw new RecordNotFoundException($"Запись объекта с PK {orderId}:{article} не найдена для обновления");

            _mapper.Map(updateDto, existingModel);
            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД об обновлении объекта");

            return true;
        }

        /// <summary>
        /// Возвращает все товары, принадлежащие конкретному заказу.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <returns>Список позиций указанного заказа.</returns>
        /// <exception cref="RecordNotFoundException">Если в данном заказе нет товаров.</exception>
        public async Task<OrderProductDto> GetAllByOrderIdAsync(int orderId)
        {
            var model = await _repository.GetAllByOrderIdAsync(orderId);

            if (model.Count == 0)
                throw new RecordNotFoundException($"Продукты заказа с ID {orderId} не найдены");

            return _mapper.Map<OrderProductDto>(model);
        }
    }
}