using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
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
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var response = await _client.GetAsync($"blogs/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound(); 
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var blog = JsonSerializer.Deserialize<ResultBlogDTO>(jsonData, _jsonOptions);

            if (blog == null)
            {
                return View(null); 
            }

            return View(blog);
        }

  


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(CreateCommentDTO dto)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewsletter(CreateNewsletterDTO dto)
        {

            if (!ModelState.IsValid)
            {
                TempData["NewsletterMessage"] = "Lütfen geçerli bir e-posta adresi giriniz.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

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

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
