using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class CommentApiService : ICommentApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CommentApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultCommentDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("comments");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultCommentDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultCommentDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"comments/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultCommentDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateCommentWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PostAsync("comments/create-admin-comment", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateCommentWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.CommentID.ToString()), "CommentID");

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PutAsync("comments", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"comments/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetBlogDropdownItemsAsync()
        {
            var response = await _httpClient.GetAsync("blogs");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var blogs = JsonSerializer.Deserialize<List<ResultBlogDTO>>(json, _jsonOptions);

            return blogs?
                .Select(b => new SelectListItem
                {
                    Text = b.Title ?? "Başlıksız",
                    Value = b.BlogID.ToString()
                }).ToList()
                ?? new List<SelectListItem>();
        }

        private MultipartFormDataContent GetFormData(CreateCommentDTO dto)
        {
            return new MultipartFormDataContent
            {
                { new StringContent(dto.UserName ?? ""), "UserName" },
                { new StringContent(dto.Email ?? ""), "Email" },
                { new StringContent(dto.Content ?? ""), "Content" },
                { new StringContent(dto.BlogID.ToString()), "BlogID" }
            };
        }

        private StreamContent GetStreamContent(IFormFile file)
        {
            var content = new StreamContent(file.OpenReadStream());
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            return content;
        }
    }
}
