using DTO.Models.Products;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebClient.WebClients.Products
{
    public class ProductsWebClient : IProductsWebClient
    {
        private readonly HttpClient _httpClient;

        public ProductsWebClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<List<ProductFullDto>> GetAllAsync(string token, int? page = null, int? pageSize = null)
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

            return await response.Content.ReadFromJsonAsync<List<ProductFullDto>>();
        }

        public virtual async Task<ProductFullDto> GetByArticleAsync(string token, string article)
        {
            SetToken(token);

            var response = await _httpClient.GetAsync($"{article}");

            await ThrowIfNotSuccess(response);

            return await response.Content.ReadFromJsonAsync<ProductFullDto>();
        }

        public virtual async Task<ProductFullDto> PostAsync(string token, ProductCreateDto createDto)
        {
            SetToken(token);

            var responce = await _httpClient.PostAsJsonAsync("", createDto);

            await ThrowIfNotSuccess(responce);

            return await responce.Content.ReadFromJsonAsync<ProductFullDto>();
        }

        public virtual async Task<bool> PutAsync(string token, string article, ProductUpdateDto updateDto)
        {
            SetToken(token);

            var responce = await _httpClient.PutAsJsonAsync($"{article}", updateDto);

            await ThrowIfNotSuccess(responce);

            return await responce.Content.ReadFromJsonAsync<bool>();
        }

        public virtual async Task<bool> DeleteAsync(string token, string article)
        {
            SetToken(token);

            var responce = await _httpClient.DeleteAsync($"{article}");

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
