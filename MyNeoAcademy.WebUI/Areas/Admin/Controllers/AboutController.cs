using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public AboutController(IHttpClientFactory httpClientFactory)
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
            var response = await _client.GetAsync("abouts");
            if (!response.IsSuccessStatusCode)
                return View(new List<ResultAboutDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultAboutDTO>>(json, _jsonOptions);
            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"abouts/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var about = JsonSerializer.Deserialize<ResultAboutDTO>(jsonData, _jsonOptions);

            return View(about);
        }

        // 🔹 Ekleme (GET)
        [HttpGet]
        public IActionResult Create() => View();

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.Subtitle ?? ""), "Subtitle" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Description ?? ""), "Description" },
                { new StringContent(dto.ButtonText ?? ""), "ButtonText" },
                { new StringContent(dto.ButtonLink ?? ""), "ButtonLink" }
            };

            if (dto.ImageFrontFile != null)
            {
                var frontContent = new StreamContent(dto.ImageFrontFile.OpenReadStream());
                frontContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFrontFile.ContentType);
                formData.Add(frontContent, "ImageFrontFile", dto.ImageFrontFile.FileName);
            }

            if (dto.ImageBackFile != null)
            {
                var backContent = new StreamContent(dto.ImageBackFile.OpenReadStream());
                backContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageBackFile.ContentType);
                formData.Add(backContent, "ImageBackFile", dto.ImageBackFile.FileName);
            }

            var response = await _client.PostAsync("abouts", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Hakkımızda bilgisi eklenemedi.");
            return View(dto);
        }

        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"abouts/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResultAboutDTO>(json, _jsonOptions);

            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateAboutWithFileDTO
            {
                AboutID = result.AboutID,
                Subtitle = result.Subtitle,
                Title = result.Title,
                Description = result.Description,
                ButtonText = result.ButtonText,
                ButtonLink = result.ButtonLink,
                ImageFrontUrl = result.ImageFrontUrl,
                ImageBackUrl = result.ImageBackUrl
            };

            return View(dto);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAboutWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.AboutID.ToString()), "AboutID" },
                { new StringContent(dto.Subtitle ?? ""), "Subtitle" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Description ?? ""), "Description" },
                { new StringContent(dto.ButtonText ?? ""), "ButtonText" },
                { new StringContent(dto.ButtonLink ?? ""), "ButtonLink" }
            };

            if (dto.ImageFrontFile != null)
            {
                var frontContent = new StreamContent(dto.ImageFrontFile.OpenReadStream());
                frontContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFrontFile.ContentType);
                formData.Add(frontContent, "ImageFrontFile", dto.ImageFrontFile.FileName);
            }

            if (dto.ImageBackFile != null)
            {
                var backContent = new StreamContent(dto.ImageBackFile.OpenReadStream());
                backContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageBackFile.ContentType);
                formData.Add(backContent, "ImageBackFile", dto.ImageBackFile.FileName);
            }

            var response = await _client.PutAsync("abouts", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Hakkımızda bilgisi güncellenemedi.");
            return View(dto);
        }

        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"abouts/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Hakkımızda bilgisi silinemedi.";
            return RedirectToAction("Index");
        }
    }
}
