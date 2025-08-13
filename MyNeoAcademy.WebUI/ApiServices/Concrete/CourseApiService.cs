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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;

        public CourseApiService(IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _httpContextAccessor = httpContextAccessor;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        private void AddBearerToken()
        {
            var token = _httpContextAccessor.HttpContext?
                .User?
                .Claims
                .FirstOrDefault(c => c.Type == "AccessToken")
                ?.Value;

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }


        public async Task<List<ResultCourseDTO>> GetCoursesByInstructorIdAsync(int instructorId)
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync($"Courses/byinstructor/{instructorId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultCourseDTO>>(json, _jsonOptions) ?? new List<ResultCourseDTO>();
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
        private MultipartFormDataContent GetFormData(CreateCourseWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
    {
        { new StringContent(dto.Title ?? ""), "Title" },
        { new StringContent(dto.Description ?? ""), "Description" },
        { new StringContent(dto.ImageUrl ?? ""), "ImageUrl" },
        { new StringContent(dto.Price?.ToString() ?? "0"), "Price" },
        { new StringContent(dto.CategoryID?.ToString() ?? ""), "CategoryID" },
        { new StringContent(dto.InstructorID?.ToString() ?? "") , "InstructorID" }
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
