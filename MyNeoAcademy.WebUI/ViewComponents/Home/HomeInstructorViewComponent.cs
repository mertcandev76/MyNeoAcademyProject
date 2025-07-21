using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeInstructorViewComponent:ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomeInstructorViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("Instructors");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultInstructorDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var instructors = await JsonSerializer.DeserializeAsync<List<ResultInstructorDTO>>(stream, options)
                             ?? new List<ResultInstructorDTO>();

            return View(instructors);
        }

    }
}
