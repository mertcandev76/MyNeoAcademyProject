using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.MessageDTOs;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MessageController : Controller
    {
        private readonly HttpClient _client;
        private readonly IValidator<CreateMessageDTO> _createValidator;
        private readonly IValidator<UpdateMessageDTO> _updateValidator;

        public MessageController(IHttpClientFactory httpClientFactory, IValidator<CreateMessageDTO> createValidator, IValidator<UpdateMessageDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // GET: List all messages
        public async Task<IActionResult> Index()
        {
            var messages = await _client.GetFromJsonAsync<List<ResultMessageDTO>>("messages");
            return View(messages);
        }

        // GET: Details of one message
        public async Task<IActionResult> Details(int id)
        {
            var message = await _client.GetFromJsonAsync<ResultMessageDTO>($"messages/{id}");
            if (message == null) return NotFound();
            return View(message);
        }

        // GET: Show create form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new message
        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PostAsJsonAsync("messages", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Mesaj eklenirken bir hata oluştu.");
            return View(dto);
        }

        // GET: Show edit form
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var message = await _client.GetFromJsonAsync<UpdateMessageDTO>($"messages/{id}");
            if (message == null) return NotFound();

            return View(message);
        }

        // POST: Update message
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateMessageDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PutAsJsonAsync("messages", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }

        // GET: Delete message
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"messages/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}
