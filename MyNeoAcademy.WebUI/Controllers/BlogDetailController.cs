using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.Models;
using System.Text;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogDetailController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id, int commentPage = 1)
        {
            try
            {
                var response = await _client.GetAsync($"blogs/{id}");
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                var blogJson = await response.Content.ReadAsStringAsync();
                var blog = JsonSerializer.Deserialize<ResultBlogDTO>(blogJson, _jsonOptions);

                if (blog == null)
                    return View(null);

                ViewBag.CommentPage = commentPage;
                return View(blog);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Blog detayları yüklenemedi: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(CreateCommentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Yorum bilgileri geçerli değil.";
                return RedirectToAction("Detail", new { id = dto.BlogID });
            }

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("comments/create-user-comment", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Detail", new { id = dto.BlogID });
                }

                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = "Yorum eklenemedi: " + error;
                return RedirectToAction("Detail", new { id = dto.BlogID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Yorum ekleme sırasında hata oluştu: " + ex.Message;
                return RedirectToAction("Detail", new { id = dto.BlogID });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewsletter(CreateNewsletterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["NewsletterMessage"] = "Lütfen geçerli bir e-posta adresi giriniz.";
                return SafeRedirectBack();
            }

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("newsletters", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["NewsletterMessage"] = "Abonelik başarılı bir şekilde gerçekleştirildi.";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    TempData["NewsletterMessage"] = "Abonelik başarısız oldu: " + error;
                }
            }
            catch (Exception ex)
            {
                TempData["NewsletterMessage"] = "Abonelik sırasında bir hata oluştu: " + ex.Message;
            }

            return SafeRedirectBack();
        }

        private IActionResult SafeRedirectBack()
        {
            var referer = Request.Headers["Referer"].ToString();
            return Url.IsLocalUrl(referer) ? Redirect(referer) : RedirectToAction("Index", "Home");
        }
    }
}
