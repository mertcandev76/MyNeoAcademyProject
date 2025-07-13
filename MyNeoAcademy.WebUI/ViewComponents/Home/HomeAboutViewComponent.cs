using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;

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

            var abouts = await client.GetFromJsonAsync<List<ResultAboutDTO>>("abouts")
                         ?? new List<ResultAboutDTO>();

            return View(abouts);
        }
    }
}
