using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Text.Json;
using System.Text;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CategoryApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultCategoryDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultCategoryDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultCategoryDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultCategoryDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateCategoryDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("categories", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateCategoryDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("categories", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetDropdownItemsAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<ResultCategoryDTO>>(json, _jsonOptions);

            return categories?
                .Select(c => new SelectListItem
                {
                    Text = c.Name ?? "Kategori Yok",
                    Value = c.CategoryID.ToString()
                }).ToList()
                ?? new List<SelectListItem>();
        }
    }
}
