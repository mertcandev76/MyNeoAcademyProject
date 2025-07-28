using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogSearchViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogSearchViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string? query = HttpContext.Request.Query["query"].ToString();

            if (string.IsNullOrWhiteSpace(query))
                return View(new List<ResultBlogDTO>());

            var searchResults = new List<ResultBlogDTO>();

            var response = await _httpClient.GetAsync($"api/blog/search?query={System.Net.WebUtility.UrlEncode(query)}");

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                searchResults = await JsonSerializer.DeserializeAsync<List<ResultBlogDTO>>(responseStream,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

            return View(searchResults);
        }
    }
}
