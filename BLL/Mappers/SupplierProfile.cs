using AutoMapper;
using DAL.Efcore.Models;
using DTO.Models.Suppliers;

namespace BLL.Mappers
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierFullDto>().ReverseMap();
            CreateMap<Supplier, SupplierActionDto>().ReverseMap();
        }
    }
}
