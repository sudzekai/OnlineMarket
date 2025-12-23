using BLL.Services.Service;
using DTO.Models.Clients;

namespace BLL.Services.Clients
{
    public interface IClientsService : IService<ClientFullDto, ClientCreateDto, ClientUpdateDto>
    {
        Task<ClientFullDto> GetByLoginAndPasswordAsync(string login, string password);
        Task<ClientAuthDto> GetByLoginAsync(string login);
    }
}