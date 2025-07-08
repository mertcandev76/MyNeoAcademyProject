using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TestimonialController : Controller
    {
        private readonly HttpClient _client;
        private readonly IValidator<CreateTestimonialDTO> _createValidator;
        private readonly IValidator<UpdateTestimonialDTO> _updateValidator;

        public TestimonialController(IHttpClientFactory httpClientFactory,
                                     IValidator<CreateTestimonialDTO> createValidator,
                                     IValidator<UpdateTestimonialDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // Listeleme
        public async Task<IActionResult> Index()
        {
            var list = await _client.GetFromJsonAsync<List<ResultTestimonialDTO>>("testimonials");
            return View(list);
        }

        // Detay
        public async Task<IActionResult> Details(int id)
        {
            var item = await _client.GetFromJsonAsync<ResultTestimonialDTO>($"testimonials/{id}");
            if (item == null) return NotFound();
            return View(item);
        }

        // Yeni kayıt formu (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni kayıt ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var err in validationResult.Errors)
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                return View(dto);
            }

            var response = await _client.PostAsJsonAsync("testimonials", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Kayıt eklenirken bir hata oluştu.");
            return View(dto);
        }

        // Güncelleme formu (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _client.GetFromJsonAsync<UpdateTestimonialDTO>($"testimonials/{id}");
            if (item == null) return NotFound();
            return View(item);
        }

        // Güncelleme işlemi (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTestimonialDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var err in validationResult.Errors)
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                return View(dto);
            }

            var response = await _client.PutAsJsonAsync("testimonials", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }

        // Silme işlemi
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"testimonials/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}
