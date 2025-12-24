using API.Controllers.Controller;
using BLL.Services.Clients;
using BLL.Services.Orders;
using DTO.CompositeModels.Orders;
using DTO.Models.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для управления заказами.
    /// </summary>
    /// <remarks>
    /// Наследует базовый функционал CRUD из <see cref="Controller{TFullDto, TCreateDto, TUpdateDto}"/> 
    /// и дополняет его методами для получения агрегированных данных о заказах.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller<OrderFullDto, OrderCreateDto, OrderUpdateDto>
    {
        private readonly new IOrdersService _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrdersController"/>.
        /// </summary>
        /// <param name="service">Сервис заказов.</param>
        /// <param name="clientsService">Сервис клиентов (передается в базовый контроллер).</param>
        public OrdersController(IOrdersService service, IClientsService clientsService) : base(service, clientsService)
        {
            _service = service;
        }

        /// <summary>
        /// Получает полную детализированную информацию о заказе по его идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор заказа.</param>
        /// <returns>
        /// Возвращает объект <see cref="OrderFullInfoDto"/>, включающий данные клиента, 
        /// список товаров с расчитанными скидками и итоговые суммы.
        /// </returns>
        /// <response code="200">Информация о заказе успешно сформирована.</response>
        /// <response code="400">Ошибка при получении данных (например, заказ не найден).</response>
        [HttpGet("{id}/fullinfo")]
        [ProducesResponseType(typeof(OrderFullInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderFullInfoDto>> GetFullInfoByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetFullInfoByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Примечание: В будущем рекомендуется использовать Middleware для обработки исключений
                return BadRequest(ex.Message);
            }
        }
    }
}