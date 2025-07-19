using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Entity.Entities;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogCommentFormViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogCommentFormViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync(int blogId)
        {
            var response = await _httpClient.GetAsync($"blogs/{blogId}");

            if (!response.IsSuccessStatusCode)
                return View(new Blog());

            var stream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var blog = await JsonSerializer.DeserializeAsync<Blog>(stream, options)
                       ?? new Blog();

            return View(blog);
        }
    }
}
