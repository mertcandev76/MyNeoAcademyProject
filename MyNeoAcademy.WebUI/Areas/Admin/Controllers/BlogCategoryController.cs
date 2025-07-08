using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]/[Controller]/[Action]/{id?}")]
    public class BlogCategoryController : Controller
    {

        private readonly HttpClient _client;
        private readonly IValidator<CreateBlogCategoryDTO> _createValidator;
        private readonly IValidator<CreateBlogCategoryDTO> _updateValidator;

        public BlogCategoryController(IHttpClientFactory httpClientFactory,
                              IValidator<CreateBlogCategoryDTO> createValidator,
                              IValidator<CreateBlogCategoryDTO> updateValidator)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<ResultBlogCategoryDTO>>("blogcategories");
            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetFromJsonAsync<ResultBlogCategoryDTO>($"blogcategories/{id}");
            if (response == null) return NotFound();
            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogCategoryDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PostAsJsonAsync("blogcategories", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Kategori eklenirken bir hata oluştu.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetFromJsonAsync<UpdateBlogCategoryDTO>($"blogcategories/{id}");
            if (response == null)
                return NotFound();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogCategoryDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(dto);
            }

            var response = await _client.PutAsJsonAsync("blogcategories", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"blogcategories/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}
