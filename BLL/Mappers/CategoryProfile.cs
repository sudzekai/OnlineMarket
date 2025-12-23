using AutoMapper;
using DAL.Efcore.Models;
using DTO.Models.Categories;

namespace BLL.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryFullDto>().ReverseMap();
            CreateMap<Category, CategoryActionDto>().ReverseMap();
        }
    }
}
