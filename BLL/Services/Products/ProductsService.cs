using AutoMapper;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Products;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.Products;

namespace BLL.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        public ProductsService(IUnitOfWork uow, IProductsRepository repository, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ProductFullDto>> GetAllAsync()
        {
            var models = await _repository.GetAllAsync();

            if (models is null || models.Count == 0)
                throw new Exception("Записи объекта не найдены");

            return _mapper.Map<List<ProductFullDto>>(models);
        }

        public async Task<List<ProductFullDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var models = await _repository.GetAllPagedAsync(page, pageSize);

            if (models is null || models.Count == 0)
                throw new Exception("Записи объекта не найдены");

            return _mapper.Map<List<ProductFullDto>>(models);
        }

        public async Task<ProductFullDto?> GetByArticleAsync(string article)
        {
            var model = await _repository.GetByArticleAsync(article)
                ?? throw new Exception($"Запись товара с артикулом {article} не найдена");

            return _mapper.Map<ProductFullDto>(model);
        }

        public virtual async Task<ProductFullDto> AddAsync(ProductCreateDto createDto)
        {
            var model = _mapper.Map<Product>(createDto);

            var createdModel = await _repository.AddAsync(model)
                ?? throw new Exception("Ошибка при создании записи объекта");

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new Exception("Ошибка при сохранении изменений в БД о создании объекта");

            return _mapper.Map<ProductFullDto>(createdModel);
        }

        public virtual async Task<bool> DeleteAsync(string article)
        {
            var isDeleted = await _repository.DeleteAsync(article);

            if (!isDeleted)
                throw new Exception("Ошибка при удалении записи объекта");

            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new Exception("Ошибка при сохранении изменений в БД об удалении объекта");

            return isDeleted;
        }

        public virtual async Task<bool> UpdateAsync(int orderId, string article, ProductUpdateDto updateDto)
        {
            var existingModel = await _repository.GetByArticleAsync(article)
                ?? throw new Exception($"Запись товара с артикулом {article} не найдена для обновления");

            _mapper.Map(updateDto, existingModel);
            var result = await _uow.SaveChangesAsync();

            if (result.Equals(0))
                throw new Exception("Ошибка при сохранении изменений в БД об обновлении объекта");

            return true;
        }
    }
}
