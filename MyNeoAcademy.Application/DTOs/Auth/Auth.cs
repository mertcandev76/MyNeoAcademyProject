using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs.Auth
{
    public class LoginDTO
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }

    public class RegisterDTO
    {
        public string Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public IFormFile? ProfileImageFile { get; set; }
    }
    public class TokenResultDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();
    }

}
