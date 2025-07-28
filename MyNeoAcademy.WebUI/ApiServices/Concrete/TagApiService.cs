using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Text.Json;
using System.Text;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class TagApiService : ITagApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TagApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultTagDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("tags");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultTagDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultTagDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"tags/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultTagDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateTagDTO dto)
        {
            var content = GetJsonContent(dto);
            var response = await _httpClient.PostAsync("tags", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateTagDTO dto)
        {
            var content = GetJsonContent(dto);
            var response = await _httpClient.PutAsync("tags", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"tags/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetDropdownItemsAsync()
        {
            var response = await _httpClient.GetAsync("tags");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var tags = JsonSerializer.Deserialize<List<ResultTagDTO>>(json, _jsonOptions);

            return tags?
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.TagID.ToString()
                }).ToList()
                ?? new List<SelectListItem>();
        }

        private StringContent GetJsonContent<T>(T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
