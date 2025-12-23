using BLL.Services.Service;
using DTO.Models.Suppliers;

namespace BLL.Services.Suppliers
{
    public interface ISuppliersService : IService<SupplierFullDto, SupplierActionDto, SupplierActionDto>
    {
    }
}