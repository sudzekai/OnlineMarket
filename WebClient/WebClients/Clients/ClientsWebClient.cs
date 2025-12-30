using DTO.Models.Clients;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Clients
{
    public class ClientsWebClient : WebClientBase<ClientFullDto, ClientCreateDto, ClientUpdateDto>, IClientsWebClient
    {
        public ClientsWebClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
