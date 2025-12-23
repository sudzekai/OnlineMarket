using BLL.Services.Clients;
using BLL.Services.Products;
using DTO.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для управления товарами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;
        private readonly IClientsService _clientsService;

        public ProductsController(IProductsService service, IClientsService clientsService)
        {
            _service = service;
            _clientsService = clientsService;
        }

        /// <summary>
        /// Получает список объектов с поддержкой опциональной пагинации.
        /// </summary>
        /// <param name="page">Номер запрашиваемой страницы (необязательно).</param>
        /// <param name="pageSize">Количество элементов на странице (необязательно). Если не указан при наличии page, по умолчанию используется 5.</param>
        /// <remarks>
        /// Если параметры пагинации не переданы, метод возвращает полный список объектов.
        /// <hr/>
        /// <ol>
        /// <li>GET /api/products (для полного списка)</li>
        /// <li>GET /api/products?page=1&amp;pageSize=10 (для 1 страницы с 10 записями)</li>
        /// <li>GET /api/products?page=1 (для 1 страницы с 5 записями)</li>
        /// <li>GET /api/products?pageSize=20 (для 20 записей с начала)</li>
        /// </ol>
        /// </remarks>
        /// <returns>Список объектов типа ProductFullDto.</returns>
        /// <response code="200">Успешное получение списка объектов.</response>
        /// <response code="204">Объекты не найдены (пустой список).</response>
        /// <response code="400">Ошибка в логике запроса или при обработке данных.</response>
        /// <response code="401">Пользователь не авторизован.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public virtual async Task<ActionResult<List<ProductFullDto>>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            try
            {
                List<ProductFullDto> models;

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
        /// Получает данные товара по его артикулу.
        /// </summary>
        /// <param name="article">Уникальный артикул товара.</param>
        /// <returns>Полная информация о товаре.</returns>
        /// <response code="200">Товар найден.</response>
        /// <response code="400">Товар не найден или ошибка запроса.</response>
        [HttpGet("{article}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<ProductFullDto>> GetById(string article)
        {
            try
            {
                var model = await _service.GetByArticleAsync(article);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Добавляет новый товар в систему.
        /// </summary>
        /// <param name="createDto">Данные нового товара.</param>
        /// <returns>Созданный товар.</returns>
        /// <response code="200">Товар успешно создан.</response>
        /// <response code="400">Некорректные данные или ошибка валидации.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<ProductFullDto>> PostAsync(ProductCreateDto createDto)
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
        /// Обновляет информацию о товаре по его артикулу.
        /// </summary>
        /// <param name="article">Артикул товара для обновления.</param>
        /// <param name="updateDto">Новые данные товара.</param>
        /// <returns>Результат выполнения операции (true/false).</returns>
        /// <response code="200">Обновление прошло успешно.</response>
        [HttpPut("{article}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<bool>> PutAsync(string article, ProductUpdateDto updateDto)
        {
            try
            {
                var result = await _service.UpdateAsync(article, updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет товар из системы.
        /// </summary>
        /// <param name="article">Артикул товара.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Товар удален.</response>
        [HttpDelete("{article}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<bool>> DeleteAsync(string article)
        {
            try
            {
                var result = await _service.DeleteAsync(article);
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