using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeTestimonialViewComponent:ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomeTestimonialViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("testimonials");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultTestimonialDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var testimonials = await JsonSerializer.DeserializeAsync<List<ResultTestimonialDTO>>(stream, options)
                                 ?? new List<ResultTestimonialDTO>();

            return View(testimonials);
        }

    }
}
