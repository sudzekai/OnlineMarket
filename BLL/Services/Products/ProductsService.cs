using AutoMapper;
using BLL.Types.Exceptions;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Products;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.Products;

namespace BLL.Services.Products
{
    /// <summary>
    /// Сервис для управления товарами в системе.
    /// </summary>
    /// <remarks>
    /// Реализует бизнес-логику работы с сущностями <see cref="Product"/>, 
    /// включая получение списков, поиск по артикулу и управление жизненным циклом записей.
    /// </remarks>
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ProductsService"/>.
        /// </summary>
        /// <param name="uow">Единица работы для фиксации транзакций.</param>
        /// <param name="repository">Специализированный репозиторий для работы с товарами.</param>
        /// <param name="mapper">Экземпляр AutoMapper для преобразования моделей в DTO.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если какой-либо из параметров равен <c>null</c>.</exception>
        public ProductsService(IUnitOfWork uow, IProductsRepository repository, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Возвращает полный список всех товаров.
        /// </summary>
        /// <returns>Список товаров в формате <see cref="ProductFullDto"/>.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если в базе данных отсутствуют записи о товарах.</exception>
        public async Task<List<ProductFullDto>> GetAllAsync()
        {
            var models = await _repository.GetAllAsync();

            if (models is null || models.Count == 0)
                throw new RecordNotFoundException("Записи объекта не найдены");

            return _mapper.Map<List<ProductFullDto>>(models);
        }

        /// <summary>
        /// Возвращает постраничный список товаров.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Количество элементов на одной странице.</param>
        /// <returns>Список товаров для указанной страницы.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если на запрашиваемой странице отсутствуют товары.</exception>
        public async Task<List<ProductFullDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var models = await _repository.GetAllPagedAsync(page, pageSize);

            if (models is null || models.Count == 0)
                throw new RecordNotFoundException("Записи объекта не найдены");

            return _mapper.Map<List<ProductFullDto>>(models);
        }

        /// <summary>
        /// Находит информацию о товаре по его уникальному артикулу.
        /// </summary>
        /// <param name="article">Артикул товара.</param>
        /// <returns>DTO товара <see cref="ProductFullDto"/>.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если товар с указанным артикулом не найден.</exception>
        public async Task<ProductFullDto?> GetByArticleAsync(string article)
        {
            var model = await _repository.GetByArticleAsync(article)
                ?? throw new RecordNotFoundException($"Запись товара с артикулом {article} не найдена");

            return _mapper.Map<ProductFullDto>(model);
        }

        /// <summary>
        /// Добавляет новый товар в систему.
        /// </summary>
        /// <param name="createDto">Данные для создания нового товара.</param>
        /// <returns>Данные созданного товара.</returns>
        /// <exception cref="RecordCreationException">Выбрасывается, если репозиторий не смог создать запись.</exception>
        /// <exception cref="RecordSavingException">Выбрасывается, если изменения не были сохранены в базе данных.</exception>
        public virtual async Task<ProductFullDto> AddAsync(ProductCreateDto createDto)
        {
            var model = _mapper.Map<Product>(createDto);

            var createdModel = await _repository.AddAsync(model)
                ?? throw new RecordCreationException("Ошибка при создании записи объекта");

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД о создании объекта");

            return _mapper.Map<ProductFullDto>(createdModel);
        }

        /// <summary>
        /// Удаляет товар из системы по его артикулу.
        /// </summary>
        /// <param name="article">Артикул удаляемого товара.</param>
        /// <returns><c>true</c>, если удаление успешно завершено.</returns>
        /// <exception cref="RecordDeletionException">Выбрасывается, если товар не был найден или не может быть удален.</exception>
        /// <exception cref="RecordSavingException">Выбрасывается при ошибке фиксации удаления в базе данных.</exception>
        public virtual async Task<bool> DeleteAsync(string article)
        {
            var isDeleted = await _repository.DeleteAsync(article);

            if (!isDeleted)
                throw new RecordDeletionException("Ошибка при удалении записи объекта");

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД об удалении объекта");

            return isDeleted;
        }

        /// <summary>
        /// Обновляет данные существующего товара.
        /// </summary>
        /// <param name="article">Артикул обновляемого товара.</param>
        /// <param name="updateDto">DTO с новыми данными.</param>
        /// <returns><c>true</c>, если обновление прошло успешно.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если товар с указанным артикулом не существует.</exception>
        /// <exception cref="RecordSavingException">Выбрасывается, если база данных не зафиксировала изменений.</exception>
        public virtual async Task<bool> UpdateAsync(string article, ProductUpdateDto updateDto)
        {
            var existingModel = await _repository.GetByArticleAsync(article)
                ?? throw new RecordNotFoundException($"Запись товара с артикулом {article} не найдена для обновления");

            _mapper.Map(updateDto, existingModel);
            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД об обновлении объекта");

            return true;
        }
    }
}