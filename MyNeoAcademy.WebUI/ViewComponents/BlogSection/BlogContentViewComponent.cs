using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogContentViewComponent:ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogContentViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            //var response = await _httpClient.GetAsync($"blogs/{id}"); // API'de bu endpoint olmalı
            var response = await _httpClient.GetAsync("blogs/1"); // API'de bu endpoint olmalı

            if (!response.IsSuccessStatusCode)
                return View(new ResultBlogDTO()); // Boş model döner

            var stream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var blog = await JsonSerializer.DeserializeAsync<ResultBlogDTO>(stream, options)
                        ?? new ResultBlogDTO();

            return View(blog);
        }
    }
}
