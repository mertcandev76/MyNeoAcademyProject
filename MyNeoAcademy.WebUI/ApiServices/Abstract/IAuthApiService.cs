using MyNeoAcademy.Application.DTOs.Auth;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IAuthApiService
    {
        Task<TokenResultDTO?> LoginAsync(LoginDTO dto);
        Task<TokenResultDTO?> RegisterAsync(RegisterDTO dto);
    }
}