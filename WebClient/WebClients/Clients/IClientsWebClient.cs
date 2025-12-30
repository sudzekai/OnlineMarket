using DTO.Models.Clients;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Clients
{
    public interface IClientsWebClient : IWebClientBase<ClientFullDto, ClientCreateDto, ClientUpdateDto>
    {
    }
}