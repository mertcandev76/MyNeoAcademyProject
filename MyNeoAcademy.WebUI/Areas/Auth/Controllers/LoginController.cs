using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs.Auth;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Security.Claims;
using System.Text.Json;
using MyNeoAcademy.WebUI.Areas.Auth.Models;


namespace MyNeoAcademy.WebUI.Areas.Auth.Controllers
{
    [Area("Auth")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class LoginController : Controller
    {
        private readonly IAuthApiService _authApiService;

        public LoginController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        [HttpGet]
        public IActionResult Index() => View(); 

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new LoginDTO
            {
                Email = model.Email,
                Password = model.Password
            };

            var tokenResult = await _authApiService.LoginAsync(dto);

            if (tokenResult == null)
            {
                ModelState.AddModelError("", "Giriş başarısız.");
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
            if (tokenResult.InstructorId.HasValue)
            {
                claims.Add(new Claim("InstructorId", tokenResult.InstructorId.Value.ToString()));
            }


            foreach (var role in tokenResult.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            if (tokenResult.Roles.Contains("Admin"))
                return RedirectToAction("Index", "About", new { area = "Admin" });
            else if (tokenResult.Roles.Contains("Author"))
                return RedirectToAction("Index", "Dashboard", new { area = "Author" });
            else if (tokenResult.Roles.Contains("Instructor"))
                return RedirectToAction("Index", "Dashboard", new { area = "Instructor" });
            else if (tokenResult.Roles.Contains("Moderator"))
                return RedirectToAction("Index", "Panel", new { area = "Moderator" });
            else if (tokenResult.Roles.Contains("User"))
                return RedirectToAction("Index", "Home", new { area = "" });
            else
                return RedirectToAction("AccessDenied", "Error");
        }



        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}








