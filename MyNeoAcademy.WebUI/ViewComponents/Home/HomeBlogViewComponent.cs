using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.Home
{
    //public class HomeBlogViewComponent:ViewComponent
    //{
    //    private readonly HttpClient _httpClient;

    //    public HomeBlogViewComponent(IHttpClientFactory httpClientFactory)
    //    {
    //        _httpClient = httpClientFactory.CreateClient("MyApiClient");
    //    }

    //    public async Task<IViewComponentResult> InvokeAsync()
    //    {
    //        var response = await _httpClient.GetAsync("blogs");

    //        if (!response.IsSuccessStatusCode)
    //            return View(new List<ResultBlogDTO>());

    //        var stream = await response.Content.ReadAsStreamAsync();

    //        var options = new JsonSerializerOptions
    //        {
    //            PropertyNameCaseInsensitive = true
    //        };

    //        var blogs = await JsonSerializer.DeserializeAsync<List<ResultBlogDTO>>(stream, options)
    //                    ?? new List<ResultBlogDTO>();

    //        return View(blogs);
    //    }
    //}
}
