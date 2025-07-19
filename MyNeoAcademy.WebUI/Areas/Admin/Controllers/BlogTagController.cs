using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogTagController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogTagController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // 🔹 Listeleme
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("blogtags");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultBlogTagDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultBlogTagDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"blogtags/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ResultBlogTagDTO>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Ekleme (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogTagDTO dto)
        {
            var response = await _client.PostAsJsonAsync("blogtags", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Eşleştirme yapılamadı.");
            await LoadDropdownsAsync();
            return View(dto);
        }

        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"blogtags/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResultBlogTagDTO>(jsonData, _jsonOptions);

            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateBlogTagDTO
            {
                BlogTagID = result.BlogTagID,
                BlogID = result.BlogID,
                TagID = result.TagID
            };

            await LoadDropdownsAsync();
            return View(dto);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogTagDTO dto)
        {
            var response = await _client.PutAsJsonAsync("blogtags", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme başarısız.");
            await LoadDropdownsAsync();
            return View(dto);
        }

        // 🔹 Silme
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"blogtags/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Silme işlemi başarısız.";
            return RedirectToAction("Index");
        }

        // 🔹 Dropdownları yükleyen yardımcı metot
        private async Task LoadDropdownsAsync()
        {
            ViewBag.Blogs = await DropdownHelper.GetDropdownItemsAsync<ResultBlogDTO>(
                _client,
                "blogs",
                b => b.Title!,
                b => b.BlogID.ToString()
            );

            ViewBag.Tags = await DropdownHelper.GetDropdownItemsAsync<ResultTagDTO>(
                _client,
                "tags",
                t => t.Name!,
                t => t.TagID.ToString()
            );
        }
    }
}
