using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs.Auth;
using System.Security.Claims;



namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // 🔐 POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    message = "Geçersiz giriş bilgileri",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });

            var tokenResult = await _authService.LoginAsync(dto);

            if (tokenResult == null)
                return Unauthorized(new { message = "E-posta veya şifre hatalı" });

            return Ok(tokenResult);
        }

        // 🧾 POST: api/auth/register
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    message = "Geçersiz kayıt verisi",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });

            try
            {
                var tokenResult = await _authService.RegisterAsync(dto);
                return Ok(tokenResult);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası", detail = ex.Message });
            }
        }

        // 🧪 GET: api/auth/profile
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity?.Name ?? "Bilinmiyor";
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "Yok";

            return Ok(new
            {
                UserId = userId,
                UserName = username,
                Email = email,
                Message = $"Merhaba {username}, JWT ile güvenli alana eriştiniz!"
            });
        }
    }
}



