using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

    namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
    {
        [Area("Admin")]
        [Route("[area]/[controller]/[action]/{id?}")]
    public class InstructorController : Controller
    {
        private readonly IInstructorApiService _instructorApiService;

        public InstructorController(IInstructorApiService instructorApiService)
        {
            _instructorApiService = instructorApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _instructorApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _instructorApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorWithFileDTO dto)
        {
            var result = await _instructorApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Eğitmen bilgisi eklenemedi.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _instructorApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateInstructorWithFileDTO
            {
                InstructorID = result.InstructorID,
                FullName = result.FullName,
                Title = result.Title,
                Bio = result.Bio,
                FacebookUrl = result.FacebookUrl,
                TwitterUrl = result.TwitterUrl,
                WebsiteUrl = result.WebsiteUrl,
                ImageUrl = result.ImageUrl
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateInstructorWithFileDTO dto)
        {
            var result = await _instructorApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Eğitmen bilgisi güncellenemedi.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _instructorApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}
