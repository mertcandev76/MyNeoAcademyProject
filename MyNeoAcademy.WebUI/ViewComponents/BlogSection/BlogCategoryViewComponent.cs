using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogCategoryViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogCategoryViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultCategoryDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var categories = await JsonSerializer.DeserializeAsync<List<ResultCategoryDTO>>(stream, options)
                             ?? new List<ResultCategoryDTO>();

            return View(categories);
        }
    }
}
