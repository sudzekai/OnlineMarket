using DTO.Models.Producers;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Producers
{
    public interface IProducersWebClient : IWebClientBase<ProducerFullDto, ProducerActionDto, ProducerActionDto>
    {
    }
}