using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogCommentViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogCommentViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync(int blogId)
        {
            var response = await _httpClient.GetAsync("comments");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultCommentDTO>());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var comments = await JsonSerializer.DeserializeAsync<List<ResultCommentDTO>>(stream, options)
                           ?? new List<ResultCommentDTO>();

            return View(comments);
        }

    }
}
