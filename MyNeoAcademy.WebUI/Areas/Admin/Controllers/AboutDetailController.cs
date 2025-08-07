using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.ApiServices.Concrete;
using System.Data;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class AboutDetailController : Controller
    {
        private readonly IAboutDetailApiService _aboutDetailApiService;

        public AboutDetailController(IAboutDetailApiService aboutDetailApiService)
        {
            _aboutDetailApiService = aboutDetailApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _aboutDetailApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _aboutDetailApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutDetailDTO dto)
        {
            var result = await _aboutDetailApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Hakkımızda detayı oluşturulamadı.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _aboutDetailApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateAboutDetailDTO
            {
                AboutDetailID = result.AboutDetailID,
                Title = result.Title,
                Paragraph1 = result.Paragraph1,
                Paragraph2 = result.Paragraph2
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAboutDetailDTO dto)
        {
            var result = await _aboutDetailApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Hakkımızda detayı güncellenemedi.");
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _aboutDetailApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}
