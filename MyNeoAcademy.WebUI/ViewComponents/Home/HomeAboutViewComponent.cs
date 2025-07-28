using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeAboutViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomeAboutViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("abouts");

                if (response.IsSuccessStatusCode)
                {
                    var aboutList = await response.Content.ReadFromJsonAsync<List<ResultAboutDTO>>();
                    var about = aboutList?.FirstOrDefault(); 

                    if (about != null)
                        return View(about);
                }


                return View(new ResultAboutDTO());
            }
            catch (Exception ex)
            {

                Console.WriteLine($"HomeAboutViewComponent hata: {ex.Message}");
                return View(new ResultAboutDTO());
            }
        }
    }
}
