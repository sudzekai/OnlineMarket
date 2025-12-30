using DTO.Models.Producers;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Producers
{
    public class ProducersWebClient : WebClientBase<ProducerFullDto, ProducerActionDto, ProducerActionDto>, IProducersWebClient
    {
        public ProducersWebClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
