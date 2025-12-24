using BLL.Services.Clients;
using BLL.Services.OrderProducts;
using DTO.Models.OrderProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderProductsController : ControllerBase
    {
        private readonly IOrderProductsService _service;
        private readonly IClientsService _clientsService;

        public OrderProductsController(IOrderProductsService service, IClientsService clientsService)
        {
            _service = service;
            _clientsService = clientsService;
        }

        /// <summary>
        /// Получает список позиций в заказах с поддержкой опциональной пагинации.
        /// </summary>
        /// <param name="page">Номер запрашиваемой страницы (необязательно).</param>
        /// <param name="pageSize">Количество элементов на странице (необязательно). Если не указан при наличии page, по умолчанию используется 5.</param>
        /// <remarks>
        /// Если параметры пагинации не переданы, метод возвращает полный список всех позиций во всех заказах.
        /// <hr/>
        /// <h4>Примеры запросов:</h4>
        /// <ol>
        /// <li>GET /api/orderproducts (для полного списка)</li>
        /// <li>GET /api/orderproducts?page=1&amp;pageSize=10 (для 1 страницы с 10 записями)</li>
        /// <li>GET /api/orderproducts?page=1 (для 1 страницы с 5 записями)</li>
        /// <li>GET /api/orderproducts?pageSize=20 (для 20 записей с начала)</li>
        /// </ol>
        /// </remarks>
        /// <returns>Список объектов типа OrderProductDto.</returns>
        /// <response code="200">Успешное получение списка объектов.</response>
        /// <response code="204">Позиции не найдены (пустой список).</response>
        /// <response code="400">Ошибка в логике запроса или при обработке данных.</response>
        /// <response code="401">Пользователь не авторизован.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public virtual async Task<ActionResult<List<OrderProductDto>>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            try
            {
                List<OrderProductDto> models;

                if (page.HasValue && pageSize.HasValue)
                    models = await _service.GetAllPagedAsync(page.Value, pageSize.Value);
                else if (page.HasValue)
                    models = await _service.GetAllPagedAsync(page.Value, 5);
                else if (pageSize.HasValue)
                    models = await _service.GetAllPagedAsync(1, pageSize.Value);
                else
                    models = await _service.GetAllAsync();

                return models.Any() ? Ok(models) : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получает информацию о конкретном товаре в конкретном заказе по составному ключу.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="article">Артикул товара.</param>
        /// <returns>Данные позиции заказа.</returns>
        /// <response code="200">Позиция найдена.</response>
        /// <response code="400">Запись не найдена или ошибка запроса.</response>
        [HttpGet("orderid/{orderId}/article/{article}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<OrderProductDto>> GetById(int orderId, string article)
        {
            try
            {
                var model = await _service.GetByPKAsync(orderId, article);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Добавляет товар в заказ.
        /// </summary>
        /// <param name="createDto">Данные позиции (ID заказа, артикул и количество).</param>
        /// <returns>Созданная запись позиции заказа.</returns>
        /// <response code="200">Товар успешно добавлен в заказ.</response>
        /// <response code="400">Некорректные данные (например, товар уже есть в заказе или складской дефицит).</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<OrderProductDto>> PostAsync(OrderProductDto createDto)
        {
            try
            {
                var model = await _service.AddAsync(createDto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновляет количество товара в существующем заказе.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="article">Артикул товара.</param>
        /// <param name="updateDto">Объект с новыми данными (количеством).</param>
        /// <returns>True, если обновление прошло успешно.</returns>
        /// <response code="200">Данные успешно обновлены.</response>
        [HttpPut("orderid/{orderId}/article/{article}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<bool>> PutAsync(int orderId, string article, OrderProductUpdateDto updateDto)
        {
            try
            {
                var result = await _service.UpdateAsync(orderId, article, updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет товар из состава заказа.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="article">Артикул удаляемого товара.</param>
        /// <returns>True, если позиция успешно удалена.</returns>
        /// <response code="200">Позиция удалена из заказа.</response>
        [HttpDelete("orderid/{orderId}/article/{article}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<bool>> DeleteAsync(int orderId, string article)
        {
            try
            {
                var result = await _service.DeleteAsync(orderId, article);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected async Task<bool> IsAuthorizedAsync()
        {
            try
            {
                var login = User.Identity?.Name;
                if (string.IsNullOrEmpty(login)) return false;

                var client = await _clientsService.GetByLoginAsync(login);
                return client is not null;
            }
            catch
            {
                return false;
            }
        }
    }
}