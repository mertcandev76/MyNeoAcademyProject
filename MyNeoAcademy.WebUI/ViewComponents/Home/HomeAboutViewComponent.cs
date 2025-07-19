using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeAboutViewComponent: ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomeAboutViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var response = await _httpClient.GetAsync("abouts/1"); // tek nesne döner
            //var response = await _httpClient.GetAsync("abouts/{aboutId}"); //tek nesne döner
            if (!response.IsSuccessStatusCode)
                return View(new ResultAboutDTO());

            var stream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var about = await JsonSerializer.DeserializeAsync<ResultAboutDTO>(stream, options)
                         ?? new ResultAboutDTO();

            return View(about);
        }
    }
}
