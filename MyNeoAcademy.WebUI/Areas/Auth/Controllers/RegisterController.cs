using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs.Auth;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.Areas.Auth.Models;
using MyNeoAcademy.WebUI.Models;
using System.Security.Claims;
using System.Text.Json;


namespace MyNeoAcademy.WebUI.Areas.Auth.Controllers
{
    [Area("Auth")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class RegisterController : Controller
    {
        private readonly IAuthApiService _authApiService;

        public RegisterController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(RegisterRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new RegisterDTO
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfileImageFile = model.ProfileImageFile
            };

            var tokenResult = await _authApiService.RegisterAsync(dto);

            if (tokenResult == null)
            {
                ModelState.AddModelError("", "Kayıt işlemi başarısız.");
                return View(model);
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, tokenResult.UserId.ToString()),
                new Claim(ClaimTypes.Name, tokenResult.UserName),
                new Claim(ClaimTypes.Email, tokenResult.Email),
                new Claim("FullName", tokenResult.FullName),
                new Claim("AccessToken", tokenResult.Token)
            };

            if (!string.IsNullOrWhiteSpace(tokenResult.ProfileImageUrl))
            {
                claims.Add(new Claim("ProfileImageUrl", tokenResult.ProfileImageUrl));
            }

            foreach (var role in tokenResult.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "About", new { area = "Admin" });
        }
    }
}




