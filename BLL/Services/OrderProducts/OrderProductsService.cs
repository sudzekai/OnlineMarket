using AutoMapper;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.OrderProducts;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.OrderProducts;

namespace BLL.Services.OrderProducts
{
    public class OrderProductsService : IOrderProductsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IOrderProductsRepository _repository;

        public OrderProductsService(IUnitOfWork uow, IOrderProductsRepository repository, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrderProductDto>> GetAllAsync()
        {
            var models = await _repository.GetAllAsync();

            if (models is null || models.Count == 0)
                throw new Exception("Записи объекта не найдены");

            return _mapper.Map<List<OrderProductDto>>(models);
        }

        public async Task<List<OrderProductDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var models = await _repository.GetAllPagedAsync(page, pageSize);

            if (models is null || models.Count == 0)
                throw new Exception("Записи объекта не найдены");

            return _mapper.Map<List<OrderProductDto>>(models);
        }

        public async Task<OrderProductDto?> GetByPKAsync(int orderId, string article)
        {
            var model = await _repository.GetByPKAsync(orderId, article)
                ?? throw new Exception($"Запись объекта PK {orderId}:{article} не найдена");

            return _mapper.Map<OrderProductDto>(model);
        }

        public virtual async Task<OrderProductDto> AddAsync(OrderProductDto createDto)
        {
            var model = _mapper.Map<OrderProduct>(createDto);

            var createdModel = await _repository.AddAsync(model)
                ?? throw new Exception("Ошибка при создании записи объекта");

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new Exception("Ошибка при сохранении изменений в БД о создании объекта");

            return _mapper.Map<OrderProductDto>(createdModel);
        }

        public virtual async Task<bool> DeleteAsync(int orderId, string article)
        {
            var isDeleted = await _repository.DeleteAsync(orderId, article);

            if (!isDeleted)
                throw new Exception("Ошибка при удалении записи объекта");

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new Exception("Ошибка при сохранении изменений в БД об удалении объекта");

            return isDeleted;
        }

        public virtual async Task<bool> UpdateAsync(int orderId, string article, OrderProductUpdateDto updateDto)
        {
            var existingModel = await _repository.GetByPKAsync(orderId, article)
                ?? throw new Exception($"Запись объекта с PK {orderId}:{article} не найдена для обновления");

            _mapper.Map(updateDto, existingModel);
            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new Exception("Ошибка при сохранении изменений в БД об обновлении объекта");

            return true;
        }
    }
}
