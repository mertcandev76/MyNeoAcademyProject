using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeStatisticViewComponent:ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomeStatisticViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("statistics");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultStatisticDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var statistics = await JsonSerializer.DeserializeAsync<List<ResultStatisticDTO>>(stream, options)
                             ?? new List<ResultStatisticDTO>();

            return View(statistics);
        }

    }
}
