using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebClient.WebClients.WebClient
{
    public class WebClientBase<TFullDto, TCreateDto, TUpdateDto> : IWebClientBase<TFullDto, TCreateDto, TUpdateDto>
        where TFullDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly HttpClient _httpClient;

        public WebClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<List<TFullDto>> GetAllAsync(string token, int? page = null, int? pageSize = null)
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

            return await response.Content.ReadFromJsonAsync<List<TFullDto>>();
        }

        public virtual async Task<TFullDto> GetByIdAsync(string token, int id)
        {
            SetToken(token);

            var response = await _httpClient.GetAsync($"{id}");

            await ThrowIfNotSuccess(response);

            return await response.Content.ReadFromJsonAsync<TFullDto>();
        }

        public virtual async Task<TFullDto> PostAsync(string token, TCreateDto createDto)
        {
            SetToken(token);

            var responce = await _httpClient.PostAsJsonAsync("", createDto);

            await ThrowIfNotSuccess(responce);

            return await responce.Content.ReadFromJsonAsync<TFullDto>();
        }

        public virtual async Task<bool> PutAsync(string token, int id, TUpdateDto updateDto)
        {
            SetToken(token);

            var responce = await _httpClient.PutAsJsonAsync($"{id}", updateDto);

            await ThrowIfNotSuccess(responce);

            return await responce.Content.ReadFromJsonAsync<bool>();
        }

        public virtual async Task<bool> DeleteAsync(string token, int id)
        {
            SetToken(token);

            var responce = await _httpClient.DeleteAsync($"{id}");

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
