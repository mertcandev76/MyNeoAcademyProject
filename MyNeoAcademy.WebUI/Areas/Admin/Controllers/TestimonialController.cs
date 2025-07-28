using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialApiService _testimonialApiService;

        public TestimonialController(ITestimonialApiService testimonialApiService)
        {
            _testimonialApiService = testimonialApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _testimonialApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _testimonialApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialWithFileDTO dto)
        {
            var result = await _testimonialApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Referans eklenemedi.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _testimonialApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateTestimonialWithFileDTO
            {
                TestimonialID = result.TestimonialID,
                FullName = result.FullName,
                Title = result.Title,
                Content = result.Content,
                ImageUrl = result.ImageUrl,
                Rating = result.Rating
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTestimonialWithFileDTO dto)
        {
            var result = await _testimonialApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Referans güncellenemedi.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testimonialApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}
