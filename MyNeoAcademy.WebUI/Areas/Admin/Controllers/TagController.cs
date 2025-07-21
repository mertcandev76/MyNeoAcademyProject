using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using System.Text;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TagController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public TagController(IHttpClientFactory httpClientFactory)
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
            var response = await _client.GetAsync("tags");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultTagDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultTagDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"tags/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var tag = JsonSerializer.Deserialize<ResultTagDTO>(jsonData, _jsonOptions);

            return View(tag);
        }

        // 🔹 Ekleme (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("tags", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Etiket eklenemedi.");
            return View(dto);
        }

        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"tags/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var tag = JsonSerializer.Deserialize<UpdateTagDTO>(jsonData, _jsonOptions);

            return View(tag);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTagDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("tags", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Etiket güncellenemedi.");
            return View(dto);
        }

        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"tags/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Etiket silinemedi.";
            return RedirectToAction("Index");
        }
    }
}
