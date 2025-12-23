using BLL.Services.Clients;
using BLL.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Controller
{
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

        public Controller(IService<TFullDto, TCreateDto, TUpdateDto> service, IClientsService clientsService)
        {
            _service = service;
            _clientsService = clientsService;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TFullDto>>> GetAll()
        {
            try
            {
                var models = await _service.GetAllAsync();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("page/{page}")]
        public virtual async Task<ActionResult<List<TFullDto>>> GetAllAsync(int page)
        {
            try
            {
                var models = await _service.GetAllPagedAsync(page, 5);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("page/{page}/pagesize/{pageSize}")]
        public virtual async Task<ActionResult<List<TFullDto>>> GetPagedAllAsync(int page, int pageSize)
        {
            try
            {
                var models = await _service.GetAllPagedAsync(page, pageSize);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pagesize/{pageSize}")]
        public virtual async Task<ActionResult<List<TFullDto>>> GetSpecificAmountAsync(int pageSize)
        {
            try
            {
                var models = await _service.GetAllPagedAsync(1, pageSize);
                return Ok(models);
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
                var login = User.Identity.Name;

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
