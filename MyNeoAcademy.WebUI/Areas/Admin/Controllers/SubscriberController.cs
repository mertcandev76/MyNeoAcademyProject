using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.SubscriberDTOs;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SubscriberController : Controller
    {
        private readonly HttpClient _client;
        private readonly IValidator<CreateSubscriberDTO> _createValidator;
        private readonly IValidator<UpdateSubscriberDTO> _updateValidator;

        public SubscriberController(IHttpClientFactory httpClientFactory,
                                    IValidator<CreateSubscriberDTO> createValidator,
                                    IValidator<UpdateSubscriberDTO> updateValidator)
        {

            _client = httpClientFactory.CreateClient("MyApiClient");
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // Listeleme
        public async Task<IActionResult> Index()
        {
            var list = await _client.GetFromJsonAsync<List<ResultSubscriberDTO>>("subscribers");
            return View(list);
        }

        // Detay
        public async Task<IActionResult> Details(int id)
        {
            var item = await _client.GetFromJsonAsync<ResultSubscriberDTO>($"subscribers/{id}");
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
        public async Task<IActionResult> Create(CreateSubscriberDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var err in validationResult.Errors)
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                return View(dto);
            }

            var response = await _client.PostAsJsonAsync("subscribers", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Kayıt eklenirken bir hata oluştu.");
            return View(dto);
        }

        // Güncelleme formu (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _client.GetFromJsonAsync<UpdateSubscriberDTO>($"subscribers/{id}");
            if (item == null) return NotFound();
            return View(item);
        }

        // Güncelleme işlemi (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSubscriberDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var err in validationResult.Errors)
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                return View(dto);
            }

            var response = await _client.PutAsJsonAsync("subscribers", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }

        // Silme işlemi
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"subscribers/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}

