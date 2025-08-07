using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{

    public class BlogCommentViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogCommentViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }


        public async Task<IViewComponentResult> InvokeAsync(int blogId, int page = 1, int pageSize = 4)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"comments/pagedbyblog?blogId={blogId}&page={page}&pageSize={pageSize}");

                if (!response.IsSuccessStatusCode)
                {

                    ViewData["Error"] = "Yorumlar yüklenirken bir hata oluştu.";
                    return View("Default", new PagedResultDTO<ResultCommentDTO>() { Items = new List<ResultCommentDTO>() });
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var pagedComments = await JsonSerializer.DeserializeAsync<PagedResultDTO<ResultCommentDTO>>(stream, _jsonOptions)
                                    ?? new PagedResultDTO<ResultCommentDTO>() { Items = new List<ResultCommentDTO>() };
                ViewBag.BlogID = blogId;

                return View("Default", pagedComments); 
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Beklenmeyen hata: {ex.Message}";
                return View("Default", new PagedResultDTO<ResultCommentDTO>() { Items = new List<ResultCommentDTO>() });
            }
        }
    }
}
