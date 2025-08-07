using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Menu
{
    public class InstructorListViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public InstructorListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }


        public async Task<IViewComponentResult> InvokeAsync(string displayType = "Home")
        {
            var response = await _httpClient.GetAsync("instructors");

            if (!response.IsSuccessStatusCode)
                return View("Default", new List<ResultInstructorDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var instructors = await JsonSerializer.DeserializeAsync<List<ResultInstructorDTO>>(stream, options)
                              ?? new List<ResultInstructorDTO>();


            if (displayType == "Home")
                instructors = instructors.Take(4).ToList();

            ViewData["DisplayType"] = displayType;

            return View("Default", instructors);
        }
    }
}
