using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.WebUI.ApiServices.Concrete;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
    
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorApiService _instructorApiService;
        private readonly IAppUserApiService _appUserApiService;

        public InstructorController(IInstructorApiService instructorApiService, IAppUserApiService appUserApiService)
        {
            _instructorApiService = instructorApiService;
            _appUserApiService = appUserApiService;
        }
        private async Task LoadAppUserDropdownAsync(object? selectedValue = null)
        {

            var appUserList = await _appUserApiService.GetDropdownItemsByRoleAsync("Instructor");
            ViewBag.AppUserList = new SelectList(appUserList, "Value", "Text", selectedValue);
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
        public async Task<IActionResult> Create()
        {
            await LoadAppUserDropdownAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadAppUserDropdownAsync(dto.AppUserID);
                return View(dto);
            }
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
            await LoadAppUserDropdownAsync(result.AppUserID);

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
            if (!ModelState.IsValid)
            {
                await LoadAppUserDropdownAsync(dto.AppUserID);
                return View(dto);
            }
            var result = await _instructorApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Eğitmen bilgisi güncellenemedi.");
            await LoadAppUserDropdownAsync(dto.AppUserID);
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _instructorApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}
