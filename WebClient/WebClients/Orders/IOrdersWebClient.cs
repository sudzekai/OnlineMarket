using DTO.Models.Orders;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Orders
{
    public interface IOrdersWebClient : IWebClientBase<OrderFullDto, OrderCreateDto, OrderUpdateDto>
    {
    }
}