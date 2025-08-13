using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs.User;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class CourseLikeApiService : ICourseLikeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;

        public CourseLikeApiService(IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<ResultCourseLikeDTO>> GetAllAsync()
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync("CourseLikes");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultCourseLikeDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultCourseLikeDTO?> GetByIdAsync(int id)
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync($"CourseLikes/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultCourseLikeDTO>(json, _jsonOptions);
        }

        public async Task<bool> CreateAsync(CreateCourseLikeDTO dto)
        {
            AddBearerToken();
            var response = await _httpClient.PostAsJsonAsync("CourseLikes", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _httpClient.DeleteAsync($"CourseLikes/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetCourseDropdownItemsAsync()
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync("Courses");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var courses = JsonSerializer.Deserialize<List<ResultCourseDTO>>(json, _jsonOptions);

            return courses?
                .Select(c => new SelectListItem
                {
                    Text = c.Title ?? "Başlıksız Kurs",
                    Value = c.CourseID.ToString()
                })
                .ToList() ?? new List<SelectListItem>();
        }

        public async Task<List<SelectListItem>> GetUserDropdownItemsAsync()
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync("AppUser");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<ResultAppUserDTO>>(json, _jsonOptions);

            return users?
                .Select(u => new SelectListItem
                {
                    Text = u.FullName ?? "İsimsiz Kullanıcı",
                    Value = u.Id.ToString()
                })
                .ToList() ?? new List<SelectListItem>();
        }

        public async Task<List<ResultCourseLikeDTO>> GetByInstructorIdAsync()
        {
            AddBearerToken();
            var response = await _httpClient.GetAsync("CourseLikes/byinstructor");
            if (!response.IsSuccessStatusCode)
                return new List<ResultCourseLikeDTO>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultCourseLikeDTO>>(json, _jsonOptions) ?? new List<ResultCourseLikeDTO>();
        }
    }
}

