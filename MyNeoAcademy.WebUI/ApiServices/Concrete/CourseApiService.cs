using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers; 
using System.Text.Json;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class CourseApiService : ICourseApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CourseApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultCourseDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultCourseDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultCourseDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"courses/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultCourseDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateCourseWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PostAsync("courses", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateCourseWithFileDTO dto)
        {
            var formData = GetFormData(dto);
            formData.Add(new StringContent(dto.CourseID.ToString()), "CourseID");

            if (dto.ImageFile != null)
                formData.Add(GetStreamContent(dto.ImageFile), "ImageFile", dto.ImageFile.FileName);

            var response = await _httpClient.PutAsync("courses", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"courses/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetDropdownItemsAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var courses = JsonSerializer.Deserialize<List<ResultCourseDTO>>(json, _jsonOptions);

            return courses?
                .Select(c => new SelectListItem
                {
                    Text = c.Title ?? "Başlıksız",
                    Value = c.CourseID.ToString()
                }).ToList()
                ?? new List<SelectListItem>();
        }

        private MultipartFormDataContent GetFormData(CreateCourseDTO dto)
        {
            return new MultipartFormDataContent
            {
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Description ?? ""), "Description" },
                { new StringContent(dto.ImageUrl ?? ""), "ImageUrl" },
                { new StringContent(dto.Rating.ToString()), "Rating" },
                { new StringContent(dto.ReviewCount.ToString()), "ReviewCount" },
                { new StringContent(dto.StudentCount.ToString()), "StudentCount" },
                { new StringContent(dto.LikeCount.ToString()), "LikeCount" },
                { new StringContent(dto.Price?.ToString() ?? ""), "Price" },
                { new StringContent(dto.CategoryID?.ToString() ?? ""), "CategoryID" },
                { new StringContent(dto.InstructorID?.ToString() ?? ""), "InstructorID" }
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
