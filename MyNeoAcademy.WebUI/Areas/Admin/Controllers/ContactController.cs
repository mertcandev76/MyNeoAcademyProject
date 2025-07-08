using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;
using Newtonsoft.Json.Linq;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ContactController : Controller
    {
        private readonly HttpClient _client;
        private readonly IValidator<CreateContactDTO> _createValidator;
        private readonly IValidator<UpdateContactDTO> _updateValidator;

        public ContactController(IHttpClientFactory httpClientFactory, IValidator<CreateContactDTO> createValidator, IValidator<UpdateContactDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient"); // Program.cs'teki isimle eşleşmeli
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<ResultContactDTO>>("contacts");
            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetFromJsonAsync<ResultContactDTO>($"contacts/{id}");
            if (response == null) return NotFound();
            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PostAsJsonAsync("contacts", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "İletişim alanı eklenirken bir hata oluştu.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetFromJsonAsync<UpdateContactDTO>($"contacts/{id}");
            if (response == null)
                return NotFound();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateContactDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PutAsJsonAsync("contacts", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"contacts/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}
