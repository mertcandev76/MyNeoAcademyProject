using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Menu
{
    public class BlogListViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync(string displayType = "Home", int page = 1, int pageSize = 4)
        {
            string url;

            if (displayType == "BlogMenu")
                url = $"blogs?page={page}&pageSize={pageSize}"; 
            else
                url = "blogs"; 

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return View("Default", new PagedResultDTO<ResultBlogDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var pagedResult = await JsonSerializer.DeserializeAsync<PagedResultDTO<ResultBlogDTO>>(stream, options)
                              ?? new PagedResultDTO<ResultBlogDTO>();


            if (displayType == "Home")
                pagedResult.Items = pagedResult.Items.Take(pageSize).ToList();

            ViewData["DisplayType"] = displayType;

            return View("Default", pagedResult);
        }
    }
}
