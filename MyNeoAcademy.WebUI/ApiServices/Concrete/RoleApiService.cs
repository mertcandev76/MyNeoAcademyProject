using MyNeoAcademy.Application.DTOs.Role;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class RoleApiService : IRoleApiService
    {
        private readonly HttpClient _httpClient;

        public RoleApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
        }

        public async Task<List<ResultRoleDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("role");
            if (!response.IsSuccessStatusCode)
                return new List<ResultRoleDTO>();

            var roles = await response.Content.ReadFromJsonAsync<List<ResultRoleDTO>>();
            return roles ?? new List<ResultRoleDTO>();
        }

        public async Task CreateAsync(CreateRoleDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("role", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            var roleNames = await GetRoleNamesAsync();
            return roleNames.Contains(roleName, StringComparer.OrdinalIgnoreCase);
        }

        public async Task UpdateAsync(UpdateRoleDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"role/{dto.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"role/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<string>> GetRoleNamesAsync()
        {
            var response = await _httpClient.GetAsync("role/names");
            if (!response.IsSuccessStatusCode)
                return new List<string>();

            var names = await response.Content.ReadFromJsonAsync<List<string>>();
            return names ?? new List<string>();
        }
    }
}
