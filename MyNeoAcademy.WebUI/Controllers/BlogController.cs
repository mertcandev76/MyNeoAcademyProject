using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 3)
        {
            ViewBag.CurrentPage = page;

            var response = await _client.GetAsync($"blogs?page={page}&pageSize={pageSize}");
            var jsonData = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return View(new PagedResultDTO<ResultBlogDTO>());

            var pagedResult = JsonSerializer.Deserialize<PagedResultDTO<ResultBlogDTO>>(jsonData, _jsonOptions);

            return View(pagedResult);
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
