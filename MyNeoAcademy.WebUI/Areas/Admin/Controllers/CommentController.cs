using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;
using System.Text;
using MyNeoAcademy.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CommentController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public CommentController(IHttpClientFactory httpClientFactory)
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
            var response = await _client.GetAsync("comments");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultCommentDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultCommentDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"comments/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var comment = JsonSerializer.Deserialize<ResultCommentDTO>(jsonData, _jsonOptions);

            return View(comment);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
    {
        { new StringContent(dto.UserName ?? ""), "UserName" },
        { new StringContent(dto.Email ?? ""), "Email" },
        { new StringContent(dto.Content ?? ""), "Content" },
        { new StringContent(dto.BlogID.ToString()), "BlogID" },
        { new StringContent(dto.CreatedDate.ToString("o")), "CreatedDate" }
    };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PostAsync("comments/create-admin-comment", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yorum eklenemedi.");

            await LoadDropdownsAsync();
            return View(dto);
        }


        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"comments/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var comment = JsonSerializer.Deserialize<ResultCommentDTO>(jsonData, _jsonOptions);

            if (comment == null)
                return RedirectToAction("Index");

            var dto = new UpdateCommentWithFileDTO
            {
                CommentID = comment.CommentID,
                UserName = comment.UserName,
                Email = comment.Email,
                Content = comment.Content,
                BlogID = comment.BlogID,
                CreatedDate = comment.CreatedDate,
                ImageUrl = comment.ImageUrl
            };

            await LoadDropdownsAsync();
            return View(dto);
        }


        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCommentWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
    {
        { new StringContent(dto.CommentID.ToString()), "CommentID" },
        { new StringContent(dto.UserName ?? ""), "UserName" },
        { new StringContent(dto.Email ?? ""), "Email" },
        { new StringContent(dto.Content ?? ""), "Content" },
        { new StringContent(dto.BlogID.ToString()), "BlogID" },
        { new StringContent(dto.CreatedDate.ToString("o")), "CreatedDate" }
    };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PutAsync("comments", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yorum güncellenemedi.");

            await LoadDropdownsAsync();
            return View(dto);
        }


        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"comments/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Yorum silinemedi.";
            return RedirectToAction("Index");
        }

        // 🔹 Dropdownları yükleyen yardımcı metot
        private async Task LoadDropdownsAsync()
        {
            ViewBag.Blogs = await DropdownHelper.GetDropdownItemsAsync<ResultBlogDTO>(
                _client,
                "blogs",
                b => b.Title!,
                b => b.BlogID.ToString()) ?? new List<SelectListItem>();
        }
    }
}
