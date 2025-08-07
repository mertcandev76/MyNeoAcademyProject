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
    public class AuthorController : Controller
    {
        private readonly IAuthorApiService _authorApiService;
        private readonly IAppUserApiService _appUserApiService;

        public AuthorController(IAuthorApiService authorApiService, IAppUserApiService appUserApiService)
        {
            _authorApiService = authorApiService;
            _appUserApiService = appUserApiService;
        }

        private async Task LoadAppUserDropdownAsync(object? selectedValue = null)
        {

            var appUserList = await _appUserApiService.GetDropdownItemsByRoleAsync("Author");
            ViewBag.AppUserList = new SelectList(appUserList, "Value", "Text", selectedValue);
        }



        public async Task<IActionResult> Index()
        {
            var data = await _authorApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _authorApiService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create(CreateAuthorWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadAppUserDropdownAsync(dto.AppUserID);
                return View(dto);
            }

            var result = await _authorApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yazar eklenemedi.");
            await LoadAppUserDropdownAsync(dto.AppUserID);
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _authorApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            await LoadAppUserDropdownAsync(result.AppUserID);

            var dto = new UpdateAuthorWithFileDTO
            {
                AuthorID = result.AuthorID,
                Name = result.Name,
                Bio = result.Bio,
                ImageUrl = result.ImageUrl,
                FacebookUrl = result.FacebookUrl,
                TwitterUrl = result.TwitterUrl,
                WebsiteUrl = result.WebsiteUrl,
                AppUserID = result.AppUserID
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuthorWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadAppUserDropdownAsync(dto.AppUserID);
                return View(dto);
            }

            var result = await _authorApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yazar güncellenemedi.");
            await LoadAppUserDropdownAsync(dto.AppUserID);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _authorApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Yazar silinemedi.";

            return RedirectToAction("Index");
        }
    }
}