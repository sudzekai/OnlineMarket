using BLL.Services.Clients;
using BLL.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Controller
{
    /// <summary>
    /// Базовый абстрактный контроллер для реализации стандартных CRUD-операций.
    /// </summary>
    /// <typeparam name="TFullDto">Тип DTO для отображения полной информации о сущности.</typeparam>
    /// <typeparam name="TCreateDto">Тип DTO для создания новой сущности.</typeparam>
    /// <typeparam name="TUpdateDto">Тип DTO для обновления существующей сущности.</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class Controller<TFullDto, TCreateDto, TUpdateDto> : ControllerBase
        where TFullDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly IService<TFullDto, TCreateDto, TUpdateDto> _service;

        protected readonly IClientsService _clientsService;

        protected Controller(IService<TFullDto, TCreateDto, TUpdateDto> service, IClientsService clientsService)
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
        /// <h4>Примеры запросов:</h4>
        /// <ol>
        /// <li>GET /api/controller (для полного списка)</li>
        /// <li>GET /api/controller?page=1&amp;pageSize=10 (для 1 страницы с 10 записями)</li>
        /// <li>GET /api/controller?page=1 (для 1 страницы с 5 записями)</li>
        /// <li>GET /api/controller?pageSize=20 (для 20 записей с начала)</li>
        /// </ol>
        /// </remarks>
        /// <returns>Список объектов типа <typeparamref name="TFullDto"/>.</returns>
        /// <response code="200">Успешное получение списка объектов.</response>
        /// <response code="204">Объекты не найдены (пустой список).</response>
        /// <response code="400">Ошибка в логике запроса или при обработке данных.</response>
        /// <response code="401">Пользователь не авторизован.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public virtual async Task<ActionResult<List<TFullDto>>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            try
            {
                List<TFullDto> models = [];

                if (page.HasValue && pageSize.HasValue)
                    models = await _service.GetAllPagedAsync(page.Value, pageSize.Value);
                else if (page.HasValue)
                    models = await _service.GetAllPagedAsync(page.Value, 5);
                else if (pageSize.HasValue)
                    models = await _service.GetAllPagedAsync(1, pageSize.Value);
                else
                    models = await _service.GetAllAsync();

                if (models.Count > 0)
                    return Ok(models);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получает объект по уникальному идентификатору.
        /// </summary>
        /// <param name="id">ID объекта.</param>
        /// <returns>Найденный объект.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<TFullDto>> GetById(int id)
        {
            try
            {
                var model = await _service.GetByIdAsync(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Создает новую запись.
        /// </summary>
        /// <param name="createDto">Объект с данными для создания.</param>
        /// <returns>Созданный объект с заполненным ID.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<TFullDto>> PostAsync(TCreateDto createDto)
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
        /// Обновляет существующую запись.
        /// </summary>
        /// <param name="id">ID объекта для обновления.</param>
        /// <param name="updateDto">Объект с обновленными данными.</param>
        /// <returns>True, если обновление прошло успешно.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<bool>> PutAsync(int id, TUpdateDto updateDto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет запись по ID.
        /// </summary>
        /// <param name="id">ID объекта для удаления.</param>
        /// <returns>True, если удаление прошло успешно.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Проверяет авторизацию пользователя на основе Claim в токене.
        /// </summary>
        /// <returns>True, если пользователь существует в базе данных.</returns>
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