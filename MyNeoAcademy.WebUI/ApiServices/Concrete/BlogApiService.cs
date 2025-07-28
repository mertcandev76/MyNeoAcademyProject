using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers; 
using System.Text.Json;

    public class BlogApiService:IBlogApiService

    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultBlogDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("blogs");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultBlogDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultBlogDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"blogs/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultBlogDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateBlogWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PostAsync("blogs", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateBlogWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.BlogID.ToString()), "BlogID");

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PutAsync("blogs", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"blogs/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetDropdownItemsAsync()
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

        private MultipartFormDataContent GetFormData(CreateBlogDTO dto)
        {
            return new MultipartFormDataContent
            {
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.ShortDescription ?? ""), "ShortDescription" },
                { new StringContent(dto.Content ?? ""), "Content" },
                { new StringContent(dto.ImageUrl ?? ""), "ImageUrl" },
                { new StringContent(dto.AuthorID?.ToString() ?? ""), "AuthorID" },
                { new StringContent(dto.CategoryID?.ToString() ?? ""), "CategoryID" }
            };
        }

        private StreamContent GetStreamContent(IFormFile file)
        {
            var content = new StreamContent(file.OpenReadStream());
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            return content;
        }
    }

