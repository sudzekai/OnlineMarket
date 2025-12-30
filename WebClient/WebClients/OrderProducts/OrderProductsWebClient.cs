using DTO.Models.OrderProducts;
using DTO.Models.Orders;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebClient.WebClients.OrderProducts
{
    public class OrderProductsWebClient : IOrderProductsWebClient
    {
        private readonly HttpClient _httpClient;

        public OrderProductsWebClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<List<OrderProductDto>> GetAllAsync(string token, int? page = null, int? pageSize = null)
        {
            SetToken(token);

            HttpResponseMessage response;
            if (page.HasValue && pageSize.HasValue)
                response = await _httpClient.GetAsync($"?page={page.Value}&pagesize={pageSize.Value}");
            else if (page.HasValue)
                response = await _httpClient.GetAsync($"?page={page.Value}");
            else if (pageSize.HasValue)
                response = await _httpClient.GetAsync($"?pagesize={pageSize.Value}");
            else
                response = await _httpClient.GetAsync("");

            await ThrowIfNotSuccess(response);

            return await response.Content.ReadFromJsonAsync<List<OrderProductDto>>();
        }

        public virtual async Task<OrderProductDto> GetByOrderIdAndArticleAsync(string token, int orderId, string article)
        {
            SetToken(token);

            var response = await _httpClient.GetAsync($"orderid/{orderId}/article/{article}");

            await ThrowIfNotSuccess(response);

            return await response.Content.ReadFromJsonAsync<OrderProductDto>();
        }

        public virtual async Task<OrderProductDto> PostAsync(string token, OrderProductDto createDto)
        {
            SetToken(token);

            var responce = await _httpClient.PostAsJsonAsync("", createDto);

            await ThrowIfNotSuccess(responce);

            return await responce.Content.ReadFromJsonAsync<OrderProductDto>();
        }

        public virtual async Task<bool> PutAsync(string token, int orderId, string article, OrderUpdateDto updateDto)
        {
            SetToken(token);

            var responce = await _httpClient.PutAsJsonAsync($"orderid/{orderId}/article/{article}", updateDto);

            await ThrowIfNotSuccess(responce);

            return await responce.Content.ReadFromJsonAsync<bool>();
        }

        public virtual async Task<bool> DeleteAsync(string token, int orderId, string article)
        {
            SetToken(token);

            var responce = await _httpClient.DeleteAsync($"orderid/{orderId}/article/{article}");

            await ThrowIfNotSuccess(responce);

            return await responce.Content.ReadFromJsonAsync<bool>();
        }

        private void SetToken(string token)
            => _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        private async Task ThrowIfNotSuccess(HttpResponseMessage responce)
        {
            if (!responce.IsSuccessStatusCode)
            {
                var message = await responce.Content.ReadAsStringAsync();
                throw new Exception($"{responce.StatusCode} - {responce.ReasonPhrase}\nMessage: {message}\nRequest: {responce.RequestMessage}");
            }
        }
    }
}
