using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseController : Controller
    {
        private readonly HttpClient _client;
        private readonly IValidator<CreateCourseDTO> _createValidator;
        private readonly IValidator<UpdateCourseDTO> _updateValidator;

        public CourseController(
            IHttpClientFactory httpClientFactory,
            IValidator<CreateCourseDTO> createValidator,
            IValidator<UpdateCourseDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // Listeleme
        public async Task<IActionResult> Index()
        {
            var courses = await _client.GetFromJsonAsync<List<ResultCourseDTO>>("courses");
            return View(courses);
        }

        // Detay
        public async Task<IActionResult> Details(int id)
        {
            var course = await _client.GetFromJsonAsync<ResultCourseDTO>($"courses/{id}");
            if (course == null) return NotFound();
            return View(course);
        }

        // Yeni Course (GET)
        public async Task<IActionResult> Create()
        {
            ViewBag.CourseCategories = await GetCourseCategorySelectListAsync();
            return View();
        }

        // Yeni Course (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseDTO createCourseDTO)
        {
            ValidationResult result = await _createValidator.ValidateAsync(createCourseDTO);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                ViewBag.CourseCategories = await GetCourseCategorySelectListAsync();
                return View(createCourseDTO);
            }

            var response = await _client.PostAsJsonAsync("courses", createCourseDTO);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "API'den hata döndü: " + response.ReasonPhrase);
            ViewBag.CourseCategories = await GetCourseCategorySelectListAsync();
            return View(createCourseDTO);
        }

        // Güncelle (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _client.GetFromJsonAsync<UpdateCourseDTO>($"courses/{id}");
            if (course == null) return NotFound();

            ViewBag.CourseCategories = await GetCourseCategorySelectListAsync();
            return View(course);
        }

        // Güncelle (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCourseDTO updateCourseDTO)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(updateCourseDTO);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                ViewBag.CourseCategories = await GetCourseCategorySelectListAsync();
                return View(updateCourseDTO);
            }

            var response = await _client.PutAsJsonAsync("courses", updateCourseDTO);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "API'den hata döndü: " + response.ReasonPhrase);
            ViewBag.CourseCategories = await GetCourseCategorySelectListAsync();
            return View(updateCourseDTO);
        }

        // Silme (GET)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"courses/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }

        // Dropdown için CourseCategory listesi
        private async Task<List<SelectListItem>> GetCourseCategorySelectListAsync()
        {
            var categories = await _client.GetFromJsonAsync<List<ResultCourseCategoryDTO>>("coursecategories");
            return categories?.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CourseCategoryID.ToString()
            }).ToList() ?? new List<SelectListItem>();
        }
    }
}

