using API.Controllers.Controller;
using BLL.Services.Clients;
using BLL.Services.Orders;
using BLL.Services.Service;
using DTO.CompositeModels.Orders;
using DTO.Models.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller<OrderFullDto, OrderCreateDto, OrderUpdateDto>
    {
        private readonly new IOrdersService _service;

        public OrdersController(IOrdersService service, IClientsService clientsService) : base(service, clientsService)
        {
            _service = service;
        }


        [HttpGet("{id}/fullinfo")]
        public async Task<ActionResult<OrderFullInfoDto>> GetFullInfoByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetFullInfoByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
