using MyNeoAcademy.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IAuthService
    {
        Task<TokenResultDTO> LoginAsync(LoginDTO dto);
        Task<TokenResultDTO> RegisterAsync(RegisterDTO dto);
    }
}
