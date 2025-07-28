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
            var response = await _httpClient.GetAsync("blogs");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultBlogDTO>()); // boş liste dön

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var blogList = await JsonSerializer.DeserializeAsync<List<ResultBlogDTO>>(stream, options)
                            ?? new List<ResultBlogDTO>();

            return View(blogList);
        }
    }

}
