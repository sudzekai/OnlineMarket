using API.Controllers.Controller;
using BLL.Services.Clients;
using BLL.Services.Producers;
using BLL.Services.Service;
using DAL.Efcore.Models;
using DTO.Models.Producers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : Controller<ProducerFullDto, ProducerActionDto, ProducerActionDto>
    {
        public ProducersController(IProducersService service, IClientsService clientsService) : base(service, clientsService)
        {
        }
    }
}
