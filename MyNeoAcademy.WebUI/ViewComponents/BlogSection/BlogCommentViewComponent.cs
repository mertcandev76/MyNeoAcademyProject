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

        // Varsayılan olarak page = 1, pageSize = 4
        public async Task<IViewComponentResult> InvokeAsync(int blogId, int page = 1, int pageSize = 4)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"comments/pagedbyblog?blogId={blogId}&page={page}&pageSize={pageSize}");

                if (!response.IsSuccessStatusCode)
                {
                    // View'da mesaj gösterilecekse ModelState içine hata ekleyebilirsin
                    ViewData["Error"] = "Yorumlar yüklenirken bir hata oluştu.";
                    return View("Default", new PagedResultDTO<ResultCommentDTO>() { Items = new List<ResultCommentDTO>() });
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var pagedComments = await JsonSerializer.DeserializeAsync<PagedResultDTO<ResultCommentDTO>>(stream, _jsonOptions)
                                    ?? new PagedResultDTO<ResultCommentDTO>() { Items = new List<ResultCommentDTO>() };
                ViewBag.BlogID = blogId;

                return View("Default", pagedComments); // Özel view kullanıyorsan burayı değiştir
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Beklenmeyen hata: {ex.Message}";
                return View("Default", new PagedResultDTO<ResultCommentDTO>() { Items = new List<ResultCommentDTO>() });
            }
        }
    }


    //public class BlogCommentViewComponent : ViewComponent
    //{
    //    private readonly HttpClient _httpClient;

    //    public BlogCommentViewComponent(IHttpClientFactory httpClientFactory)
    //    {
    //        _httpClient = httpClientFactory.CreateClient("MyApiClient");
    //    }
    //    public async Task<IViewComponentResult> InvokeAsync(int blogId)
    //    {
    //        var response = await _httpClient.GetAsync("comments");

    //        if (!response.IsSuccessStatusCode)
    //            return View(new List<ResultCommentDTO>());

    //        var stream = await response.Content.ReadAsStreamAsync();

    //        var options = new JsonSerializerOptions
    //        {
    //            PropertyNameCaseInsensitive = true
    //        };

    //        var comments = await JsonSerializer.DeserializeAsync<List<ResultCommentDTO>>(stream, options)
    //                       ?? new List<ResultCommentDTO>();

    //        return View(comments);
    //    }

    //}
}
