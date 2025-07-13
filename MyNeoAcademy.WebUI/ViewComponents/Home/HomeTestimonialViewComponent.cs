using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.StatisticDTOs;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeTestimonialViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeTestimonialViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var testimonials = await client.GetFromJsonAsync<List<ResultTestimonialDTO>>("testimonials")
                         ?? new List<ResultTestimonialDTO>();

            return View(testimonials);
        }
    }
}
