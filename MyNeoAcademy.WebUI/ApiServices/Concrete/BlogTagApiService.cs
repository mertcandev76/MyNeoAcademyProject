using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Text.Json;
using System.Text;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class BlogTagApiService : IBlogTagApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogTagApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultBlogTagDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("blogtags");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultBlogTagDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultBlogTagDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"blogtags/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultBlogTagDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateBlogTagDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("blogtags", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateBlogTagDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("blogtags", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"blogtags/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExistsAsync(int blogId, int tagId)
        {
            var response = await _httpClient.GetAsync($"blogtags/exists?blogId={blogId}&tagId={tagId}");
            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(json, _jsonOptions);
        }
    }
}
