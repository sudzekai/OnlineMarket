using BLL.Services.Service;
using DTO.Models.Categories;

namespace BLL.Services.Categories
{
    public interface ICategoriesService : IService<CategoryFullDto, CategoryActionDto, CategoryActionDto>
    {
    }
}