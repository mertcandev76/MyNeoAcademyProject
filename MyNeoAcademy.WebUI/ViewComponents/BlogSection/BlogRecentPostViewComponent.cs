using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogRecentPostViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogRecentPostViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var recentPosts = new List<ResultBlogDTO>();

            var response = await _httpClient.GetAsync("api/blog/recentposts"); // API endpoint adresi

            if (response.IsSuccessStatusCode)
            {
                // Stream olarak JSON'u oku
                using var responseStream = await response.Content.ReadAsStreamAsync();

                // JSON'u deserialize et
                recentPosts = await JsonSerializer.DeserializeAsync<List<ResultBlogDTO>>(responseStream,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

            return View(recentPosts);
        }
    }
}
