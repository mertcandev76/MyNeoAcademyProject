using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeInstructorViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeInstructorViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var instructors = await client.GetFromJsonAsync<List<ResultInstructorDTO>>("Instructors")
                         ?? new List<ResultInstructorDTO>();

            return View(instructors);
        }
    }
}
