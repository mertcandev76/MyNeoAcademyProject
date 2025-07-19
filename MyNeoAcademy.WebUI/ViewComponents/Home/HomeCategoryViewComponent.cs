using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeCategoryViewComponent:ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomeCategoryViewComponent(IHttpClientFactory httpClientFactory)
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
