using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeCourseViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeCourseViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var courses = await client.GetFromJsonAsync<List<ResultCourseDTO>>("courses")
                         ?? new List<ResultCourseDTO>();

            return View(courses);
        }
    }
}
