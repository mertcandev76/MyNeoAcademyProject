using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Net.Http;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Menu
{
    public class AboutDetailListViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AboutDetailListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("aboutdetails");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultAboutDetailDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var aboutDetails = await JsonSerializer.DeserializeAsync<List<ResultAboutDetailDTO>>(stream, options)
                              ?? new List<ResultAboutDetailDTO>();

            return View(aboutDetails);
        }
    }
}
