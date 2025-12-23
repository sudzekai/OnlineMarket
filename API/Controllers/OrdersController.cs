using API.Controllers.Controller;
using BLL.Services.Clients;
using BLL.Services.Service;
using DTO.Models.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller<OrderFullDto, OrderCreateDto, OrderUpdateDto>
    {
        public OrdersController(IService<OrderFullDto, OrderCreateDto, OrderUpdateDto> service, IClientsService clientsService) : base(service, clientsService)
        {
        }
    }
}
