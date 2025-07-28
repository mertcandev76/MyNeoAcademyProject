using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;
using System.Text;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutFeatureController : Controller
    {
        private readonly IAboutFeatureApiService _aboutFeatureApiService;
        private readonly IAboutApiService _aboutApiService;

        public AboutFeatureController(
            IAboutFeatureApiService aboutFeatureApiService,
            IAboutApiService aboutApiService)
        {
            _aboutFeatureApiService = aboutFeatureApiService;
            _aboutApiService = aboutApiService;
        }

        private async Task LoadAboutDropdownAsync(object? selectedValue = null)
        {
            var aboutList = await _aboutApiService.GetDropdownItemsAsync();
            ViewBag.AboutList = new SelectList(aboutList, "Value", "Text", selectedValue);
        }

        public async Task<IActionResult> Index()
        {
            var features = await _aboutFeatureApiService.GetAllAsync();
            return View(features);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var feature = await _aboutFeatureApiService.GetByIdAsync(id);
            if (feature == null)
                return RedirectToAction("Index");

            return View(feature);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadAboutDropdownAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutFeatureDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadAboutDropdownAsync();
                return View(dto);
            }

            var result = await _aboutFeatureApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Özellik eklenemedi.");
            await LoadAboutDropdownAsync();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var feature = await _aboutFeatureApiService.GetByIdAsync(id);
            if (feature == null)
                return RedirectToAction("Index");

            await LoadAboutDropdownAsync(feature.AboutID);

            var updateDto = new UpdateAboutFeatureDTO
            {
                AboutFeatureID = feature.AboutFeatureID,
                IconClass = feature.IconClass,
                Text = feature.Text,
                AboutID = feature.AboutID
            };

            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAboutFeatureDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadAboutDropdownAsync(dto.AboutID);
                return View(dto);
            }

            var result = await _aboutFeatureApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Özellik güncellenemedi.");
            await LoadAboutDropdownAsync(dto.AboutID);
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _aboutFeatureApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}

