using AutoMapper;
using BLL.Types.Exceptions;
using DAL.Efcore.Repositories.Repository;
using DAL.Efcore.Repositories.UOW;

namespace BLL.Services.Service
{
    /// <summary>
    /// Общий сервис, реализующий CRUD-операции поверх репозитория и единицы работы.
    /// Предназначен для использования с AutoMapper для преобразования между моделью и DTO.
    /// </summary>
    /// <typeparam name="TModel">Тип доменной модели.</typeparam>
    /// <typeparam name="TFullDto">Тип DTO для полной (чтение) представления.</typeparam>
    /// <typeparam name="TCreateDto">Тип DTO для создания записи.</typeparam>
    /// <typeparam name="TUpdateDto">Тип DTO для обновления записи.</typeparam>
    public class Service<TModel, TFullDto, TCreateDto, TUpdateDto> : IService<TFullDto, TCreateDto, TUpdateDto> 
        where TModel : class
        where TFullDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        /// <summary>
        /// Единица работы для управления транзакцией/сохранением изменений в БД.
        /// </summary>
        protected readonly IUnitOfWork _uow;

        /// <summary>
        /// Репозиторий для работы с сущностями типа <typeparamref name="TModel"/>.
        /// </summary>
        protected readonly IRepository<TModel> _repository;

        /// <summary>
        /// Автоперекладчик (AutoMapper) для преобразования между моделями и DTO.
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// Создаёт экземпляр сервиса.
        /// </summary>
        /// <param name="uow">Единица работы. Не может быть <c>null</c>.</param>
        /// <param name="repository">Репозиторий для модели. Не может быть <c>null</c>.</param>
        /// <param name="mapper">Экземпляр <see cref="IMapper"/>. Не может быть <c>null</c>.</param>
        /// <exception cref="System.ArgumentNullException">Если <paramref name="uow"/>, <paramref name="repository"/> или <paramref name="mapper"/> равны <c>null</c>.</exception>
        public Service(IUnitOfWork uow, IRepository<TModel> repository, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Возвращает все доступные записи в виде списка DTO.
        /// </summary>
        /// <returns>Список объектов <typeparamref name="TFullDto"/>.</returns>
        /// <exception cref="System.Exception">Если записи не найдены.</exception>
        public virtual async Task<List<TFullDto>> GetAllAsync()
        {
            var models = await _repository.GetAllAsync();

            if (models is null || models.Count == 0)
                throw new RecordNotFoundException("Записи объекта не найдены");

            return _mapper.Map<List<TFullDto>>(models);
        }

        /// <summary>
        /// Возвращает постраничный набор записей.
        /// </summary>
        /// <param name="page">Номер страницы (0-индексация или 1 — зависит от реализации репозитория).</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <returns>Список объектов <typeparamref name="TFullDto"/> для указанной страницы.</returns>
        /// <exception cref="System.Exception">Если записи не найдены.</exception>
        public virtual async Task<List<TFullDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var models = await _repository.GetAllPagedAsync(page, pageSize);

            if (models is null || models.Count == 0)
                throw new RecordNotFoundException("Записи объекта не найдены");

            return _mapper.Map<List<TFullDto>>(models);
        }

        /// <summary>
        /// Возвращает запись по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи.</param>
        /// <returns>Объект <typeparamref name="TFullDto"/>.</returns>
        /// <exception cref="System.Exception">Если запись с указанным <paramref name="id"/> не найдена.</exception>
        public virtual async Task<TFullDto> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id)
                ?? throw new RecordNotFoundException($"Запись объекта с ID {id} не найдена");

            return _mapper.Map<TFullDto>(model);
        }

        /// <summary>
        /// Создаёт новую запись на основе DTO для создания.
        /// </summary>
        /// <param name="createDto">DTO с данными для создания записи.</param>
        /// <returns>Созданный объект в представлении <typeparamref name="TFullDto"/>.</returns>
        /// <exception cref="System.Exception">Если создание или сохранение записи прошло с ошибкой.</exception>
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
        /// Удаляет запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой записи.</param>
        /// <returns><c>true</c>, если удаление успешно; в противном случае выбрасывается исключение.</returns>
        /// <exception cref="System.Exception">Если удаление или сохранение изменений прошло с ошибкой.</exception>
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
        /// Обновляет существующую запись по идентификатору на основе DTO для обновления.
        /// </summary>
        /// <param name="id">Идентификатор обновляемой записи.</param>
        /// <param name="updateDto">DTO с новыми данными для обновления.</param>
        /// <returns><c>true</c>, если обновление и сохранение прошли успешно.</returns>
        /// <exception cref="System.Exception">Если запись не найдена или сохранение изменений прошло с ошибкой.</exception>
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