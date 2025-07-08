using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.WebUI.Extensions;
using MyNeoAcademy.WebUI.Validators.BlogValidator;
using System.Net.Http;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogController : Controller
    {
       

        private readonly HttpClient _client;
        private readonly IValidator<CreateBlogDTO> _createValidator;
        private readonly IValidator<UpdateBlogDTO> _updateValidator;

        public BlogController(
            IHttpClientFactory httpClientFactory,
            IValidator<CreateBlogDTO> createValidator,
            IValidator<UpdateBlogDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient"); // Program.cs'de yapılandırılmış HttpClient
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        // Listeleme
        public async Task<IActionResult> Index()
        {
            var blogs = await _client.GetFromJsonAsync<List<ResultBlogDTO>>("blogs");
            return View(blogs);
        }
        // Detay Sayfası (GET)
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _client.GetFromJsonAsync<ResultBlogDTO>($"blogs/{id}");
            if (blog == null)
                return NotFound();

            return View(blog);
        }


        // Yeni Blog Sayfası (GET)
        public async Task<IActionResult> Create()
        {
            ViewBag.BlogCategories = await GetBlogCategorySelectListAsync();
            return View();
        }

        // Yeni Blog Kaydet (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogDTO createBlogDTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(createBlogDTO);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                ViewBag.BlogCategories = await GetBlogCategorySelectListAsync();
                return View(createBlogDTO);
            }

            var response = await _client.PostAsJsonAsync("blogs", createBlogDTO);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else
            {
                ModelState.AddModelError("", "API'den hata döndü: " + response.ReasonPhrase);
                ViewBag.BlogCategories = await GetBlogCategorySelectListAsync();
                return View(createBlogDTO);
            }
        }

        // Güncelleme Sayfası (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _client.GetFromJsonAsync<UpdateBlogDTO>($"blogs/{id}");
            if (blog == null)
                return NotFound();

            ViewBag.BlogCategories = await GetBlogCategorySelectListAsync();
            return View(blog);
        }

        // Güncelle (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateBlogDTO updateBlogDTO)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateBlogDTO);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                ViewBag.BlogCategories = await GetBlogCategorySelectListAsync();
                return View(updateBlogDTO);
            }

            var response = await _client.PutAsJsonAsync("blogs", updateBlogDTO);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else
            {
                ModelState.AddModelError("", "API'den hata döndü: " + response.ReasonPhrase);
                ViewBag.BlogCategories = await GetBlogCategorySelectListAsync();
                return View(updateBlogDTO);
            }
        }

        // Silme Onay (GET)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"blogs/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }

        // BlogCategory dropdown listesi API'den çekiliyor
        private async Task<List<SelectListItem>> GetBlogCategorySelectListAsync()
        {
            var categories = await _client.GetFromJsonAsync<List<ResultBlogCategoryDTO>>("blogcategories");
            return categories?.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.BlogCategoryID.ToString()
            }).ToList() ?? new List<SelectListItem>();
        }
    }
}

