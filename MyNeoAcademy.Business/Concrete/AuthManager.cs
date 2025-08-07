using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs.Auth;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace MyNeoAcademy.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthManager(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IConfiguration configuration,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }




        public async Task<TokenResultDTO> RegisterAsync(RegisterDTO dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                ProfileImageUrl = null
            };


            if (dto.ProfileImageFile != null)
            {

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "users");


                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);


                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ProfileImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ProfileImageFile.CopyToAsync(fileStream);
                }


                user.ProfileImageUrl = $"/uploads/users/{fileName}";
            }

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new Exception($"Kayıt sırasında hata oluştu: {errors}");
            }


            var defaultRole = "User";
            if (!await _roleManager.RoleExistsAsync(defaultRole))
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = defaultRole,
                    Description = "Default user role"
                });
            }

            await _userManager.AddToRoleAsync(user, defaultRole);

            return await CreateTokenAsync(user);
        }


        public async Task<TokenResultDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return null!;

            return await CreateTokenAsync(user);
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            return request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230"; 
        }

        private async Task<TokenResultDTO> CreateTokenAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim("FullName", user.FullName ?? "")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = _configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("JWT SecretKey is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            string imageUrl = string.IsNullOrWhiteSpace(user.ProfileImageUrl)
                ? ""
                : user.ProfileImageUrl.StartsWith("http")
                    ? user.ProfileImageUrl
                    : $"{GetBaseUrl()}/{user.ProfileImageUrl.TrimStart('/')}";

            return new TokenResultDTO
            {
                Token = tokenString,
                Expiration = expiration,
                UserId = user.Id,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                FullName = user.FullName ?? "",
                ProfileImageUrl = imageUrl,
                Roles = roles
            };
        }
    }
}


