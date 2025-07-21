using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeCourseViewComponent:ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomeCourseViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultCourseDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var courses = await JsonSerializer.DeserializeAsync<List<ResultCourseDTO>>(stream, options)
                          ?? new List<ResultCourseDTO>();

            return View(courses);
        }

    }
}
