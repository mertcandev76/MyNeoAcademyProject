using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class AuthorApiService : IAuthorApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthorApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<SelectListItem>> GetDropdownItemsAsync()
        {
            var response = await _httpClient.GetAsync("authors");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var authors = JsonSerializer.Deserialize<List<ResultAuthorDTO>>(json, _jsonOptions);

            return authors?
                .Select(a => new SelectListItem
                {
                    Text = a.Name ?? "İsim Yok",
                    Value = a.AuthorID.ToString()
                }).ToList()
                ?? new List<SelectListItem>();
        }

        public async Task<List<ResultAuthorDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("authors");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultAuthorDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultAuthorDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"authors/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultAuthorDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateAuthorWithFileDTO dto)
        {
            var formData = GetFormData(dto);

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PostAsync("authors", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateAuthorWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.AuthorID.ToString()), "AuthorID");

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PutAsync("authors", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"authors/{id}");
            return response.IsSuccessStatusCode;
        }

        // Ortak FormData üretici
        private MultipartFormDataContent GetFormData(CreateAuthorWithFileDTO dto)
        {
            return new MultipartFormDataContent
            {
                { new StringContent(dto.Name ?? ""), "Name" },
                { new StringContent(dto.Bio ?? ""), "Bio" },
                { new StringContent(dto.FacebookUrl ?? ""), "FacebookUrl" },
                { new StringContent(dto.TwitterUrl ?? ""), "TwitterUrl" },
                { new StringContent(dto.WebsiteUrl ?? ""), "WebsiteUrl" },
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
