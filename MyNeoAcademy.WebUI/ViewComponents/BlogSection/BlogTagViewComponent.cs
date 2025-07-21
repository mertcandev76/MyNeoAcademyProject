using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogTagViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogTagViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = new List<ResultTagDTO>();

            var response = await _httpClient.GetAsync("tags"); // API endpoint

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                tags = await JsonSerializer.DeserializeAsync<List<ResultTagDTO>>(responseStream,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

            return View(tags);
        }
    }
}
