using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
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

        // 🔹 Listeleme
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("blogs");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultBlogDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultBlogDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"blogs/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var blog = JsonSerializer.Deserialize<ResultBlogDTO>(jsonData, _jsonOptions);

            return View(blog);
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
        public async Task<IActionResult> Create(CreateBlogWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
    {
        { new StringContent(dto.Title ?? ""), "Title" },
        { new StringContent(dto.ShortDescription ?? ""), "ShortDescription" },
        { new StringContent(dto.Content ?? ""), "Content" },
        { new StringContent(dto.PublishDate.ToString("o")), "PublishDate" },
        { new StringContent(dto.AuthorID?.ToString() ?? ""), "AuthorID" },
        { new StringContent(dto.CategoryID?.ToString() ?? ""), "CategoryID" },
    };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PostAsync("blogs", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Blog oluşturulamadı.");

            await LoadDropdownsAsync();
            return View(dto);
        }


        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"blogs/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var blog = JsonSerializer.Deserialize<ResultBlogDTO>(jsonData, _jsonOptions);

            if (blog == null)
                return RedirectToAction("Index");

            var dto = new UpdateBlogWithFileDTO
            {
                BlogID = blog.BlogID,
                Title = blog.Title,
                ShortDescription = blog.ShortDescription,
                Content = blog.Content,
                PublishDate = blog.PublishDate,
                AuthorID = blog.AuthorID,
                CategoryID = blog.CategoryID,
                ImageUrl = blog.ImageUrl
            };

            await LoadDropdownsAsync();
            return View(dto);
        }


        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
    {
        { new StringContent(dto.BlogID.ToString()), "BlogID" },
        { new StringContent(dto.Title ?? ""), "Title" },
        { new StringContent(dto.ShortDescription ?? ""), "ShortDescription" },
        { new StringContent(dto.Content ?? ""), "Content" },
        { new StringContent(dto.PublishDate.ToString("o")), "PublishDate" },
        { new StringContent(dto.AuthorID?.ToString() ?? ""), "AuthorID" },
        { new StringContent(dto.CategoryID?.ToString() ?? ""), "CategoryID" },
    };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PutAsync("blogs", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Blog güncellenemedi.");

            await LoadDropdownsAsync();
            return View(dto);
        }


        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"blogs/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Blog silinemedi.";
            return RedirectToAction("Index");
        }

        // 🔹 Dropdownları yükleyen yardımcı metot
        private async Task LoadDropdownsAsync()
        {
            ViewBag.Categories = await DropdownHelper.GetDropdownItemsAsync<ResultCategoryDTO>(
                _client,
                "categories",
                c => c.Name!,
                c => c.CategoryID.ToString()) ?? new List<SelectListItem>();

            ViewBag.Authors = await DropdownHelper.GetDropdownItemsAsync<ResultAuthorDTO>(
                _client,
                "authors",
                a => a.Name,
                a => a.AuthorID.ToString()) ?? new List<SelectListItem>();
        }

    }
}
