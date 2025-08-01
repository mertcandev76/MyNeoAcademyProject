﻿using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogAuthorViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BlogAuthorViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync(int authorId)
        {

            var response = await _httpClient.GetAsync($"authors/{authorId}");

            if (!response.IsSuccessStatusCode)
                return View(new ResultAuthorDTO());

            var stream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var author = await JsonSerializer.DeserializeAsync<ResultAuthorDTO>(stream, options)
                         ?? new ResultAuthorDTO();

            return View(author);
        }
    }
}
