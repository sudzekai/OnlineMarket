using API.Controllers.Controller;
using BLL.Services.Clients;
using BLL.Services.Service;
using BLL.Services.Suppliers;
using DTO.Models.Suppliers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : Controller<SupplierFullDto, SupplierActionDto, SupplierActionDto>
    {
        public SuppliersController(ISuppliersService service, IClientsService clientsService) : base(service, clientsService)
        {
        }
    }
}
