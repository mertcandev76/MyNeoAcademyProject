using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers; 
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class InstructorApiService : IInstructorApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public InstructorApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<SelectListItem>> GetDropdownItemsAsync()
        {
            var response = await _httpClient.GetAsync("instructors");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var instructors = JsonSerializer.Deserialize<List<ResultInstructorDTO>>(json, _jsonOptions);

            return instructors?
                .Select(i => new SelectListItem
                {
                    Text = i.FullName ?? "İsim Yok",
                    Value = i.InstructorID.ToString()
                })
                .ToList()
                ?? new List<SelectListItem>();
        }

        public async Task<List<ResultInstructorDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("instructors");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultInstructorDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultInstructorDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"instructors/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultInstructorDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateInstructorWithFileDTO dto)
        {
            var formData = GetFormData(dto);

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PostAsync("instructors", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateInstructorWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.InstructorID.ToString()), "InstructorID");

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PutAsync("instructors", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"instructors/{id}");
            return response.IsSuccessStatusCode;
        }

        private MultipartFormDataContent GetFormData(CreateInstructorDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.FullName ?? ""), "FullName" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Bio ?? ""), "Bio" },
                { new StringContent(dto.FacebookUrl ?? ""), "FacebookUrl" },
                { new StringContent(dto.TwitterUrl ?? ""), "TwitterUrl" },
                { new StringContent(dto.WebsiteUrl ?? ""), "WebsiteUrl" }
            };
            return formData;
        }

        private StreamContent GetStreamContent(IFormFile file)
        {
            var content = new StreamContent(file.OpenReadStream());
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            return content;
        }
    }
}
