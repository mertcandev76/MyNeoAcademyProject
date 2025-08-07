using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.ApiServices.Concrete;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class TestimonialController : Controller
    {
        private readonly ITestimonialApiService _testimonialApiService;
        private readonly IAppUserApiService _appUserApiService;

        public TestimonialController(ITestimonialApiService testimonialApiService, IAppUserApiService appUserApiService)
        {
            _testimonialApiService = testimonialApiService;
            _appUserApiService = appUserApiService;
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
        public async Task<IActionResult> Create()
        {
            await LoadAppUserDropdownAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadAppUserDropdownAsync();
                return View(dto);
            }
            var result = await _testimonialApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Referans eklenemedi.");
            await LoadAppUserDropdownAsync();
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
            await LoadAppUserDropdownAsync();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTestimonialWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadAppUserDropdownAsync();
                return View(dto);
            }
            var result = await _testimonialApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Referans güncellenemedi.");
            await LoadAppUserDropdownAsync();
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _testimonialApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
        private async Task LoadAppUserDropdownAsync(object? selectedValue = null)
        {
            var appUserList = await _appUserApiService.GetDropdownItemsByRoleAsync("User"); 
            ViewBag.AppUserList = new SelectList(appUserList, "Value", "Text", selectedValue);
        }
    }
}
