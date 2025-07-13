using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    public class HomeCategoryViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeCategoryViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var categories = await client.GetFromJsonAsync<List<ResultCategoryDTO>>("categories")
                         ?? new List<ResultCategoryDTO>();

            return View(categories);
        }
    }
}
