using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeAboutViewComponent: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeAboutViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var response = await client.GetAsync("abouts/1"); // tek nesne döner
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
