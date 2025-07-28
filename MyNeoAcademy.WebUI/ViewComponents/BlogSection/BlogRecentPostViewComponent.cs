using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
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
            var response = await _httpClient.GetAsync("RecentBlogPosts");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultRecentBlogPostDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var recentPosts = await JsonSerializer.DeserializeAsync<List<ResultRecentBlogPostDTO>>(stream, options)
                              ?? new List<ResultRecentBlogPostDTO>();

            return View(recentPosts);
        }
    }

}
