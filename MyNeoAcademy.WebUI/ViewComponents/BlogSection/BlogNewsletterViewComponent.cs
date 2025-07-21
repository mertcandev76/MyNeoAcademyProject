using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogNewsletterViewComponent : ViewComponent
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogNewsletterViewComponent(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            var dto = new CreateNewsletterDTO();
            return Task.FromResult<IViewComponentResult>(View(dto));
        }
    }
}
