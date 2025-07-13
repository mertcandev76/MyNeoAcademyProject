using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.DTO.DTOs.StatisticDTOs;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeStatisticViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeStatisticViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var statistics = await client.GetFromJsonAsync<List<ResultStatisticDTO>>("statistics")
                         ?? new List<ResultStatisticDTO>();

            return View(statistics);
        }
    }
}
