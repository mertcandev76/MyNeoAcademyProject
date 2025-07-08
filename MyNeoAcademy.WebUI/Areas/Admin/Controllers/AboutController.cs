using FluentValidation;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;
using System.Security.Policy;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        private readonly HttpClient _client;
        private readonly IValidator<CreateAboutDTO> _createValidator;
        private readonly IValidator<UpdateAboutDTO> _updateValidator;

        public AboutController(IHttpClientFactory httpClientFactory, IValidator<CreateAboutDTO> createValidator, IValidator<UpdateAboutDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient"); // Program.cs'teki isimle eşleşmeli
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        //Listeleme Sayfası
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<ResultAboutDTO>>("abouts");
            return View(response);
        }

        // Detay Sayfası
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetFromJsonAsync<ResultAboutDTO>($"abouts/{id}");
            if (response == null) return NotFound();
            return View(response);
        }

        // Ekleme sayfası - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Ekleme sayfası - POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PostAsJsonAsync("abouts", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Hakkımda alanı eklenirken bir hata oluştu.");
            return View(dto);
        }

        // Güncelleme sayfası - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetFromJsonAsync<UpdateAboutDTO>($"abouts/{id}");
            if (response == null)
                return NotFound();

            return View(response);
        }

        // Güncelleme sayfası - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateAboutDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PutAsJsonAsync("abouts", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }
        // Silme işlemi - GET (Opsiyonel: silme onayı için)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"abouts/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}
