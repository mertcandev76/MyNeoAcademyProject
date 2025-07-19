using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AuthorController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthorController(IHttpClientFactory httpClientFactory)
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
            var response = await _client.GetAsync("authors");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultAuthorDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultAuthorDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"authors/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var author = JsonSerializer.Deserialize<ResultAuthorDTO>(jsonData, _jsonOptions);

            return View(author);
        }

        // 🔹 Ekleme (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.Name ?? ""), "Name" },
                { new StringContent(dto.Bio ?? ""), "Bio" },
                { new StringContent(dto.FacebookUrl ?? ""), "FacebookUrl" },
                { new StringContent(dto.TwitterUrl ?? ""), "TwitterUrl" },
                { new StringContent(dto.WebsiteUrl ?? ""), "WebsiteUrl" }
            };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PostAsync("authors", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yazar eklenemedi.");
            return View(dto);
        }

        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"authors/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResultAuthorDTO>(jsonData, _jsonOptions);

            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateAuthorWithFileDTO
            {
                AuthorID = result.AuthorID,
                Name = result.Name,
                Bio = result.Bio,
                FacebookUrl = result.FacebookUrl,
                TwitterUrl = result.TwitterUrl,
                WebsiteUrl = result.WebsiteUrl,
                ImageUrl = result.ImageUrl
            };

            return View(dto);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuthorWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.AuthorID.ToString()), "AuthorID" },
                { new StringContent(dto.Name ?? ""), "Name" },
                { new StringContent(dto.Bio ?? ""), "Bio" },
                { new StringContent(dto.FacebookUrl ?? ""), "FacebookUrl" },
                { new StringContent(dto.TwitterUrl ?? ""), "TwitterUrl" },
                { new StringContent(dto.WebsiteUrl ?? ""), "WebsiteUrl" }
            };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PutAsync("authors", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yazar güncellenemedi.");
            return View(dto);
        }

        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"authors/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Yazar silinemedi.";
            return RedirectToAction("Index");
        }
    }
}