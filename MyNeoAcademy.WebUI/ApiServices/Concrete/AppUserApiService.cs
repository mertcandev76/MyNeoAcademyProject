using MyNeoAcademy.Application.DTOs.User;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class AppUserApiService : IAppUserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AppUserApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ResultAppUserDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("AppUser");
            if (!response.IsSuccessStatusCode)
                return new List<ResultAppUserDTO>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultAppUserDTO>>(json, _jsonOptions)!;
        }

        public async Task<ResultAppUserDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"AppUser/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResultAppUserDTO>(json, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(UpdateAppUserDTO dto)
        {
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent(dto.Id.ToString()), nameof(dto.Id));
            form.Add(new StringContent(dto.FirstName), nameof(dto.FirstName));
            form.Add(new StringContent(dto.LastName), nameof(dto.LastName));
            form.Add(new StringContent(dto.UserName), nameof(dto.UserName));
            form.Add(new StringContent(dto.Email), nameof(dto.Email));
            form.Add(new StringContent(dto.IsActive.ToString()), nameof(dto.IsActive));

            if (dto.ProfileImageFile != null)
            {
                var stream = dto.ProfileImageFile.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ProfileImageFile.ContentType);
                form.Add(fileContent, nameof(dto.ProfileImageFile), dto.ProfileImageFile.FileName);
            }

            var response = await _httpClient.PutAsync("AppUser", form);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"AppUser/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AssignRolesAsync(AssignRolesDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("AppUser/AssignRoles", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetDropdownItemsAsync()
        {
            var response = await _httpClient.GetAsync("AppUser");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<ResultAppUserDTO>>(json, _jsonOptions);

            return users?
                .Select(u => new SelectListItem
                {
                    Text = $"{u.FullName} - {u.Email}",
                    Value = u.Id.ToString()
                }).ToList()
                ?? new List<SelectListItem>();
        }

        public async Task<List<SelectListItem>> GetDropdownItemsByRoleAsync(string roleName)
        {
            var response = await _httpClient.GetAsync($"AppUser/ByRole/{roleName}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<ResultAppUserDTO>>(json, _jsonOptions);

            return users?
                .Select(u => new SelectListItem
                {
                    Text = $"{u.FullName} - {u.Email}",
                    Value = u.Id.ToString()
                }).ToList()
                ?? new List<SelectListItem>();
        }
    }
}
