using AutoMapper;
using BLL.Services.Service;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Categories;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.Categories;

namespace BLL.Services.Categories
{
    public class CategoriesService : Service<Category, CategoryFullDto, CategoryActionDto, CategoryActionDto>, ICategoriesService
    {
        private readonly new ICategoriesRepository _repository;

        public CategoriesService(IUnitOfWork uow, ICategoriesRepository repository, IMapper mapper) : base(uow, repository, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
