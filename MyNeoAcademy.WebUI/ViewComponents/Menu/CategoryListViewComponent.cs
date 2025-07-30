using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Menu
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public CategoryListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync(string displayType = "Default")
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
                return View("Default", new List<ResultCategoryDTO>());

            var stream = await response.Content.ReadAsStreamAsync();
            var categories = await JsonSerializer.DeserializeAsync<List<ResultCategoryDTO>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ResultCategoryDTO>();

            ViewData["DisplayType"] = displayType;

            return View("Default", categories);
        }
    }
}
