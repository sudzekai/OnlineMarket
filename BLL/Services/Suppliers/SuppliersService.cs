using AutoMapper;
using BLL.Services.Service;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Suppliers;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.Suppliers;

namespace BLL.Services.Suppliers
{
    public class SuppliersService : Service<Supplier, SupplierFullDto, SupplierActionDto, SupplierActionDto>, ISuppliersService
    {
        private readonly new ISuppliersRepository _repository;

        public SuppliersService(IUnitOfWork uow, ISuppliersRepository repository, IMapper mapper) : base(uow, repository, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
