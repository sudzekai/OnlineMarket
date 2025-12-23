using BLL.Services.Service;
using DAL.Efcore.Models;
using DTO.Models.Clients;

namespace BLL.Services.Clients
{
    public interface IClientsService : IService<ClientFullDto, ClientCreateDto, ClientUpdateDto>
    {
    }
}