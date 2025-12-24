using API.Controllers.Controller;
using BLL.Services.Clients;
using BLL.Services.Service;
using DTO.Models.Clients;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : Controller<ClientFullDto, ClientCreateDto, ClientUpdateDto>
    {
        public ClientsController(IService<ClientFullDto, ClientCreateDto, ClientUpdateDto> service, IClientsService clientsService) : base(service, clientsService)
        {
        }
    }
}
