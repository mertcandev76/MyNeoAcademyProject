using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using System.Net.Http;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseCategoryController : Controller
    {
        private readonly HttpClient _client;
        private readonly IValidator<CreateCourseCategoryDTO> _createValidator;
        private readonly IValidator<UpdateCourseCategoryDTO> _updateValidator;

        public CourseCategoryController(IHttpClientFactory httpClientFactory, IValidator<CreateCourseCategoryDTO> createValidator, IValidator<UpdateCourseCategoryDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<ResultCourseCategoryDTO>>("coursecategories");
            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetFromJsonAsync<ResultCourseCategoryDTO>($"coursecategories/{id}");
            if (response == null) return NotFound();
            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseCategoryDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PostAsJsonAsync("coursecategories", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "İletişim alanı eklenirken bir hata oluştu.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetFromJsonAsync<UpdateCourseCategoryDTO>($"coursecategories/{id}");
            if (response == null)
                return NotFound();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCourseCategoryDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PutAsJsonAsync("coursecategories", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"coursecategories/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");


            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}
