using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs.Auth;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.ApiServices.Concrete
{
    public class AuthApiService : IAuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MyApiClient");
        }

        public async Task<TokenResultDTO?> LoginAsync(LoginDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", dto);
            if (!response.IsSuccessStatusCode)
                return null;

            var tokenResult = await response.Content.ReadFromJsonAsync<TokenResultDTO>();
            return tokenResult;
        }

        public async Task<TokenResultDTO?> RegisterAsync(RegisterDTO dto)
        {
            System.IO.File.AppendAllText("debug.log", $"[AuthApiService] RegisterAsync called at {DateTime.Now} with: Email={dto.Email}, UserName={dto.UserName}, FirstName={dto.FirstName}, LastName={dto.LastName}, Password={dto.Password}, ConfirmPassword={dto.ConfirmPassword}, ProfileImageFile={(dto.ProfileImageFile != null ? dto.ProfileImageFile.FileName : "null")}\n");

            using var formData = new MultipartFormDataContent
    {
        { new StringContent(dto.FirstName ?? string.Empty), nameof(dto.FirstName) },
        { new StringContent(dto.LastName ?? string.Empty), nameof(dto.LastName) },
        { new StringContent(dto.Email ?? string.Empty), nameof(dto.Email) },
        { new StringContent(dto.UserName ?? string.Empty), nameof(dto.UserName) },
        { new StringContent(dto.Password ?? string.Empty), nameof(dto.Password) },
        { new StringContent(dto.ConfirmPassword ?? string.Empty), nameof(dto.ConfirmPassword) }
    };

            if (dto.ProfileImageFile != null)
            {
                var stream = new StreamContent(dto.ProfileImageFile.OpenReadStream());
                stream.Headers.ContentType = new MediaTypeHeaderValue(dto.ProfileImageFile.ContentType);
                formData.Add(stream, nameof(dto.ProfileImageFile), dto.ProfileImageFile.FileName);
            }

            var response = await _httpClient.PostAsync("auth/register", formData);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                System.IO.File.AppendAllText("debug.log", $"[AuthApiService] Register failed at {DateTime.Now}: {error}\n");
                return null;
            }

            var tokenResult = await response.Content.ReadFromJsonAsync<TokenResultDTO>();
            return tokenResult;
        }
    }
}

