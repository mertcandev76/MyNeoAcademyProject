using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Text.Json;
using System.Text;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{

    public class NewsletterApiService : INewsletterApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public NewsletterApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultNewsletterDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("newsletters");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultNewsletterDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultNewsletterDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"newsletters/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultNewsletterDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateNewsletterDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("newsletters", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateNewsletterDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("newsletters", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"newsletters/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
