using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Menu
{
    public class CourseListViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public CourseListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync(string displayType = "Home", int categoryId = 0)
        {
            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
                return View("Default", new List<ResultCourseDTO>());

            var stream = await response.Content.ReadAsStreamAsync();
            var courses = await JsonSerializer.DeserializeAsync<List<ResultCourseDTO>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<ResultCourseDTO>();

            // Eğer kategori seçildiyse filtre uygula
            if (categoryId > 0)
            {
                courses = courses.Where(c => c.Category != null && c.Category.CategoryID == categoryId).ToList();
            }
            else if (displayType == "Home")
            {
                courses = courses.Take(3).ToList();
            }

            ViewData["DisplayType"] = displayType;
            return View("Default", courses);
        }

    }

}
