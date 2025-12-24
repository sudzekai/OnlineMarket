using BLL.Services.Clients;
using DTO.CompositeModels.Requests;
using DTO.Models.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для управления сессиями пользователей и аутентификации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IClientsService _service;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="LoginController"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с клиентами.</param>
        /// <param name="configuration">Конфигурация приложения для доступа к настройкам JWT.</param>
        public LoginController(IClientsService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        /// <summary>
        /// Выполняет вход пользователя в систему и выдает JWT-токен.
        /// </summary>
        /// <param name="request">Модель запроса, содержащая логин и пароль.</param>
        /// <returns>
        /// Возвращает <see cref="LoginResponce"/> с токеном и краткой информацией о пользователе в случае успеха.
        /// </returns>
        /// <response code="200">Успешная аутентификация.</response>
        /// <response code="401">Неверные учетные данные.</response>
        [HttpPost]
        [ProducesResponseType(typeof(LoginResponce), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponce>> LoginAsync(LoginRequest request)
        {
            var client = await _service.GetByLoginAndPasswordAsync(request.Login, request.Password);

            if (client is null)
                return Unauthorized("Неверный логин или пароль");

            var token = GenerateJwtToken(client);
            return Ok(new LoginResponce()
            {
                Token = token,
                ClientFullName = client.FullName,
                ClientId = client.Id,
                Role = client.Role
            });
        }

        /// <summary>
        /// Генерирует JSON Web Token (JWT) для авторизованного пользователя.
        /// </summary>
        /// <param name="user">Данные клиента, для которого создается токен.</param>
        /// <returns>Строковое представление JWT-токена.</returns>
        /// <remarks>
        /// Токен включает в себя утверждения (Claims) о имени пользователя и его роли.
        /// Срок действия токена устанавливается в настройках (по умолчанию 1 день).
        /// </remarks>
        private string GenerateJwtToken(ClientFullDto user)
        {
            var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Role)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}