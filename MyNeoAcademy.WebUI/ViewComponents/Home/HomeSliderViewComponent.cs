using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeSliderViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomeSliderViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("Sliders");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultSliderDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var sliders = await JsonSerializer.DeserializeAsync<List<ResultSliderDTO>>(stream, options)
                          ?? new List<ResultSliderDTO>();

            return View(sliders);
        }

    }
}
