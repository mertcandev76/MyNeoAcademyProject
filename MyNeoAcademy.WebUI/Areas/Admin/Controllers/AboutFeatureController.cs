using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.AboutFeatureDTOs;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;
using System.Text;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutFeatureController : Controller
    {

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public AboutFeatureController(IHttpClientFactory httpClientFactory)
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
            var response = await _client.GetAsync("aboutfeatures");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultAboutFeatureDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultAboutFeatureDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"aboutfeatures/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var aboutFeature = JsonSerializer.Deserialize<ResultAboutFeatureDTO>(jsonData, _jsonOptions);

            return View(aboutFeature);
        }

        // 🔹 Ekleme (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Abouts = await DropdownHelper.GetDropdownItemsAsync<ResultAboutDTO>(
                _client,
                "abouts",
                a => a.Title!,
                a => a.AboutID.ToString());

            return View();
        }

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutFeatureDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Abouts = await DropdownHelper.GetDropdownItemsAsync<ResultAboutDTO>(
                    _client,
                    "abouts",
                    a => a.Title!,
                    a => a.AboutID.ToString());

                return View(dto);
            }

            var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("aboutfeatures", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "AboutFeature could not be created.");

            ViewBag.Abouts = await DropdownHelper.GetDropdownItemsAsync<ResultAboutDTO>(
                _client,
                "abouts",
                a => a.Title!,
                a => a.AboutID.ToString());

            return View(dto);
        }

        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"aboutfeatures/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var aboutFeature = JsonSerializer.Deserialize<UpdateAboutFeatureDTO>(jsonData, _jsonOptions);

            if (aboutFeature == null)
                return RedirectToAction("Index");

            ViewBag.Abouts = await DropdownHelper.GetDropdownItemsAsync<ResultAboutDTO>(
                _client,
                "abouts",
                a => a.Title!,
                a => a.AboutID.ToString());

            return View(aboutFeature);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAboutFeatureDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Abouts = await DropdownHelper.GetDropdownItemsAsync<ResultAboutDTO>(
                    _client,
                    "abouts",
                    a => a.Title!,
                    a => a.AboutID.ToString());

                return View(dto);
            }

            var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("aboutfeatures", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "AboutFeature could not be updated.");

            ViewBag.Abouts = await DropdownHelper.GetDropdownItemsAsync<ResultAboutDTO>(
                _client,
                "abouts",
                a => a.Title!,
                a => a.AboutID.ToString());

            return View(dto);
        }

        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"aboutfeatures/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "AboutFeature could not be deleted.";
            return RedirectToAction("Index");
        }
    }
}
