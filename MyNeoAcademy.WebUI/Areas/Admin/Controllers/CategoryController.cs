    using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using System.Text.Json;
using System.Text;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using System.Net.Http;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CategoryController : Controller
    {
        private readonly HttpClient _client;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
        }


        // Listeleme
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultCategoryDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();

            var categories = JsonSerializer.Deserialize<List<ResultCategoryDTO>>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return View(categories);
        }
        // Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"categories/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<ResultCategoryDTO>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (dto == null)
                return RedirectToAction("Index");

            return View(dto);
        }

        // Ekleme (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("categories", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Kategori eklenemedi.");
            return View(dto);
        }

        // Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"categories/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<UpdateCategoryDTO>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return View(dto);
        }

        // Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("categories", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Kategori güncellenemedi.");
            return View(dto);
        }

        // Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"categories/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            TempData["Error"] = "Slider silinemedi.";
            return RedirectToAction("Index");
        }

       
    }

}
