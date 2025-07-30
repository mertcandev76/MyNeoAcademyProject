using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Text.Json;
using System.Text;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class AboutDetailApiService : IAboutDetailApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AboutDetailApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultAboutDetailDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("aboutdetails");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultAboutDetailDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultAboutDetailDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"aboutdetails/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultAboutDetailDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateAboutDetailDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("aboutdetails", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateAboutDetailDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("aboutdetails", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"aboutdetails/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
