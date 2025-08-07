using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using MyNeoAcademy.WebUI.ApiServices.Concrete;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class AboutController : Controller
    {
        private readonly IAboutApiService _aboutApiService;

        public AboutController(IAboutApiService aboutApiService)
        {
            _aboutApiService = aboutApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _aboutApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _aboutApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutWithFileDTO dto)
        {
            var result = await _aboutApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Hakkımızda bilgisi eklenemedi.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _aboutApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateAboutWithFileDTO
            {
                AboutID = result.AboutID,
                Subtitle = result.Subtitle,
                Title = result.Title,
                Description = result.Description,
                ButtonText = result.ButtonText,
                ButtonLink = result.ButtonLink,
                ImageFrontUrl = result.ImageFrontUrl,
                ImageBackUrl = result.ImageBackUrl
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAboutWithFileDTO dto)
        {
            var result = await _aboutApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Hakkımızda bilgisi güncellenemedi.");
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _aboutApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }

    }
}
