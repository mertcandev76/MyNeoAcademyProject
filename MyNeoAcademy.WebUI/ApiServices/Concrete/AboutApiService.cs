using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class AboutApiService : IAboutApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AboutApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<List<SelectListItem>> GetDropdownItemsAsync()
        {
            var response = await _httpClient.GetAsync("abouts");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var abouts = JsonSerializer.Deserialize<List<ResultAboutDTO>>(json, _jsonOptions);

            return abouts?
                .Select(a => new SelectListItem
                {
                    Text = a.Title ?? "Başlık Yok",
                    Value = a.AboutID.ToString()
                }).ToList()
                ?? new List<SelectListItem>();
        }


        public async Task<List<ResultAboutDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("abouts");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultAboutDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultAboutDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"abouts/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultAboutDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateAboutWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            if (dto.ImageFrontFile != null)
                formData.Add(GetStreamContent(dto.ImageFrontFile), "ImageFrontFile", dto.ImageFrontFile.FileName);
            if (dto.ImageBackFile != null)
                formData.Add(GetStreamContent(dto.ImageBackFile), "ImageBackFile", dto.ImageBackFile.FileName);

            var response = await _httpClient.PostAsync("abouts", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateAboutWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.AboutID.ToString()), "AboutID");

            if (dto.ImageFrontFile != null)
                formData.Add(GetStreamContent(dto.ImageFrontFile), "ImageFrontFile", dto.ImageFrontFile.FileName);
            if (dto.ImageBackFile != null)
                formData.Add(GetStreamContent(dto.ImageBackFile), "ImageBackFile", dto.ImageBackFile.FileName);

            var response = await _httpClient.PutAsync("abouts", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"abouts/{id}");
            return response.IsSuccessStatusCode;
        }

        private MultipartFormDataContent GetFormData(CreateAboutWithFileDTO dto)
        {
            return new MultipartFormDataContent
            {
                { new StringContent(dto.Subtitle ?? ""), "Subtitle" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Description ?? ""), "Description" },
                { new StringContent(dto.ButtonText ?? ""), "ButtonText" },
                { new StringContent(dto.ButtonLink ?? ""), "ButtonLink" }
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
