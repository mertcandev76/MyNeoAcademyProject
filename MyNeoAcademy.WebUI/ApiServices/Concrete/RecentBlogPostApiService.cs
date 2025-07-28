using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class RecentBlogPostApiService : IRecentBlogPostApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public RecentBlogPostApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultRecentBlogPostDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("recentblogposts");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultRecentBlogPostDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultRecentBlogPostDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"recentblogposts/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultRecentBlogPostDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateRecentBlogPostWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PostAsync("recentblogposts", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateRecentBlogPostWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.RecentBlogPostID.ToString()), "RecentBlogPostID");

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PutAsync("recentblogposts", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"recentblogposts/{id}");
            return response.IsSuccessStatusCode;
        }

        private MultipartFormDataContent GetFormData(CreateRecentBlogPostDTO dto)
        {
            return new MultipartFormDataContent
            {
                { new StringContent(dto.CompactTitle), "CompactTitle" },
                { new StringContent(dto.ThumbnailUrl ?? ""), "ThumbnailUrl" }
            };
        }

        private StreamContent GetStreamContent(IFormFile file)
        {
            var content = new StreamContent(file.OpenReadStream());
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            return content;
        }
    }
}
