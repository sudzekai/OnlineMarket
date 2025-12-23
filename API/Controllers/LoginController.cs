using BLL.Services.Clients;
using DTO.CompositeModels.Requests;
using DTO.Models.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IClientsService _service;
        private readonly IConfiguration _configuration;

        public LoginController(IClientsService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost]
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
