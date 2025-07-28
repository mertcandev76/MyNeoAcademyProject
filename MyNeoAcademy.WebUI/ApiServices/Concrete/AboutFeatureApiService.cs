using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Text.Json;
using System.Text;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class AboutFeatureApiService : IAboutFeatureApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AboutFeatureApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<ResultAboutFeatureDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("aboutfeatures");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultAboutFeatureDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultAboutFeatureDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"aboutfeatures/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultAboutFeatureDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateAboutFeatureDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("aboutfeatures", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateAboutFeatureDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("aboutfeatures", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"aboutfeatures/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

