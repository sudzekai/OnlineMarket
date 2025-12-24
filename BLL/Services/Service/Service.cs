using AutoMapper;
using BLL.Types.Exceptions;
using DAL.Efcore.Repositories.Repository;
using DAL.Efcore.Repositories.UOW;

namespace BLL.Services.Service
{
    /// <summary>
    /// Базовый сервис для выполнения CRUD-операций.
    /// </summary>
    /// <remarks>
    /// Обеспечивает мост между репозиториями и контроллерами, используя <see cref="IUnitOfWork"/> для управления транзакциями 
    /// и <see cref="IMapper"/> для преобразования доменных моделей в DTO.
    /// </remarks>
    /// <typeparam name="TModel">Тип сущности базы данных.</typeparam>
    /// <typeparam name="TFullDto">DTO для передачи полной информации (Read).</typeparam>
    /// <typeparam name="TCreateDto">DTO с данными для создания (Create).</typeparam>
    /// <typeparam name="TUpdateDto">DTO с данными для обновления (Update).</typeparam>
    public class Service<TModel, TFullDto, TCreateDto, TUpdateDto> : IService<TFullDto, TCreateDto, TUpdateDto>
        where TModel : class
        where TFullDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IRepository<TModel> _repository;
        protected readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса.
        /// </summary>
        /// <param name="uow">Единица работы для фиксации изменений.</param>
        /// <param name="repository">Репозиторий для доступа к данным <typeparamref name="TModel"/>.</param>
        /// <param name="mapper">Экземпляр AutoMapper.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если один из входных сервисов равен <c>null</c>.</exception>
        public Service(IUnitOfWork uow, IRepository<TModel> repository, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Получает все записи в виде списка DTO.
        /// </summary>
        /// <returns>Список объектов <typeparamref name="TFullDto"/>.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если коллекция пуста или не существует.</exception>
        public virtual async Task<List<TFullDto>> GetAllAsync()
        {
            var models = await _repository.GetAllAsync();

            if (models is null || models.Count == 0)
                throw new RecordNotFoundException("Записи объекта не найдены");

            return _mapper.Map<List<TFullDto>>(models);
        }

        /// <summary>
        /// Получает страницу записей.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Количество элементов на странице.</param>
        /// <returns>Список <typeparamref name="TFullDto"/> для текущей страницы.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если на данной странице нет данных.</exception>
        public virtual async Task<List<TFullDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var models = await _repository.GetAllPagedAsync(page, pageSize);

            if (models is null || models.Count == 0)
                throw new RecordNotFoundException("Записи объекта не найдены");

            return _mapper.Map<List<TFullDto>>(models);
        }

        /// <summary>
        /// Находит одну запись по идентификатору.
        /// </summary>
        /// <param name="id">Первичный ключ записи.</param>
        /// <returns>Данные записи в формате <typeparamref name="TFullDto"/>.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если запись с таким <paramref name="id"/> отсутствует.</exception>
        public virtual async Task<TFullDto> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id)
                ?? throw new RecordNotFoundException($"Запись объекта с ID {id} не найдена");

            return _mapper.Map<TFullDto>(model);
        }

        /// <summary>
        /// Добавляет новую запись в базу данных.
        /// </summary>
        /// <param name="createDto">Данные для создания.</param>
        /// <returns>Полная информация о созданной записи.</returns>
        /// <exception cref="RecordCreationException">Выбрасывается при ошибке на этапе добавления в репозиторий.</exception>
        /// <exception cref="RecordSavingException">Выбрасывается, если база данных отклонила сохранение изменений.</exception>
        public virtual async Task<TFullDto> AddAsync(TCreateDto createDto)
        {
            var model = _mapper.Map<TModel>(createDto);

            var createdModel = await _repository.AddAsync(model)
                ?? throw new RecordCreationException();

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД о создании объекта");

            return _mapper.Map<TFullDto>(createdModel);
        }

        /// <summary>
        /// Удаляет запись из базы данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи для удаления.</param>
        /// <returns><c>true</c>, если удаление прошло успешно.</returns>
        /// <exception cref="RecordDeletionException">Выбрасывается, если репозиторий не смог найти или удалить запись.</exception>
        /// <exception cref="RecordSavingException">Выбрасывается при ошибке фиксации удаления в БД.</exception>
        public virtual async Task<bool> DeleteAsync(int id)
        {
            var isDeleted = await _repository.DeleteAsync(id);

            if (!isDeleted)
                throw new RecordDeletionException();

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД об удалении объекта");

            return isDeleted;
        }

        /// <summary>
        /// Обновляет данные существующей записи.
        /// </summary>
        /// <param name="id">Идентификатор записи.</param>
        /// <param name="updateDto">DTO с обновленными полями.</param>
        /// <returns><c>true</c>, если изменения успешно применены и сохранены.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если запись для обновления не найдена.</exception>
        /// <exception cref="RecordSavingException">Выбрасывается, если БД не зафиксировала изменений (например, данные идентичны или произошла ошибка).</exception>
        public virtual async Task<bool> UpdateAsync(int id, TUpdateDto updateDto)
        {
            var existingModel = await _repository.GetByIdAsync(id)
                ?? throw new RecordNotFoundException($"Запись объекта с ID {id} не найдена для обновления");

            _mapper.Map(updateDto, existingModel);
            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new RecordSavingException("Ошибка при сохранении изменений в БД об обновлении объекта");

            return true;
        }
    }
}