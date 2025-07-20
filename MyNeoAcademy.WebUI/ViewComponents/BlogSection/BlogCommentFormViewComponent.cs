using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogCommentFormViewComponent : ViewComponent
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogCommentFormViewComponent(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public Task<IViewComponentResult> InvokeAsync(int blogId)
        {
            var dto = new CreateCommentDTO
            {
                BlogID = blogId,
                CreatedDate = DateTime.Now
            };

            return Task.FromResult<IViewComponentResult>(View(dto));
        }

    }
}
