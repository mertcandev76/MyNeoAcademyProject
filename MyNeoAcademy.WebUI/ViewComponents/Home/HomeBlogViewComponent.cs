using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeBlogViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeBlogViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var blogs = await client.GetFromJsonAsync<List<ResultBlogDTO>>("blogs")
                         ?? new List<ResultBlogDTO>();

            return View(blogs);
        }
    }
}
