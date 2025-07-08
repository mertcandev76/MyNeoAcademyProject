using FluentValidation;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.BannerDTOs;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]/[Controller]/[Action]/{id?}")]

    public class BannerController : Controller
    {
        private readonly HttpClient _client;
        private readonly IValidator<CreateBannerDTO> _createValidator;
        private readonly IValidator<UpdateBannerDTO> _updateValidator;

        public BannerController(IHttpClientFactory httpClientFactory, IValidator<CreateBannerDTO> createValidator, IValidator<UpdateBannerDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<ResultBannerDTO>>("banners");
            return View(response);
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetFromJsonAsync<ResultBannerDTO>($"banners/{id}");
            if (response == null) return NotFound();
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBannerDTO dto)
        {
           
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PostAsJsonAsync("banners", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Afiş alanı eklenirken bir hata oluştu.");
            return View(dto);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetFromJsonAsync<UpdateBannerDTO>($"banners/{id}");
            if (response == null)
                return NotFound();

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBannerDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PutAsJsonAsync("banners", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"banners/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}
