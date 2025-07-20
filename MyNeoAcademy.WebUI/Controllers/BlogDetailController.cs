using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
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
                return NotFound(); // Blog bulunamadı
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var blog = JsonSerializer.Deserialize<ResultBlogDTO>(jsonData, _jsonOptions);

            if (blog == null)
            {
                return View(null); // View'da model null kontrolü varsa hata vermez
            }

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(CreateCommentDTO dto)
        {
            dto.CreatedDate = DateTime.Now;

            var formData = new MultipartFormDataContent
{
    { new StringContent(dto.UserName ?? ""), "UserName" },
    { new StringContent(dto.Email ?? ""), "Email" },
    { new StringContent(dto.Content ?? ""), "Content" },
    { new StringContent(dto.BlogID.ToString()), "BlogID" }
};

            var response = await _client.PostAsync("comments/create-user-comment", formData);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Detail", new { id = dto.BlogID });
            }

            var error = await response.Content.ReadAsStringAsync();
            TempData["ErrorMessage"] = "Yorum eklenemedi: " + error;
            return RedirectToAction("Detail", new { id = dto.BlogID });
        }


    }
}
