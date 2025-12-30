using DTO.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Orders
{
    public class OrdersWebClient : WebClientBase<OrderFullDto, OrderCreateDto, OrderUpdateDto>, IOrdersWebClient
    {
        public OrdersWebClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
