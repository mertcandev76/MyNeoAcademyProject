using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AuthorController : Controller
    {
        private readonly IAuthorApiService _authorApiService;

        public AuthorController(IAuthorApiService authorApiService)
        {
            _authorApiService = authorApiService;
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
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorWithFileDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authorApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yazar eklenemedi.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _authorApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateAuthorWithFileDTO
            {
                AuthorID = result.AuthorID,
                Name = result.Name,
                Bio = result.Bio,
                ImageUrl = result.ImageUrl,
                FacebookUrl = result.FacebookUrl,
                TwitterUrl = result.TwitterUrl,
                WebsiteUrl = result.WebsiteUrl
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuthorWithFileDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authorApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yazar güncellenemedi.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _authorApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Yazar silinemedi.";

            return RedirectToAction("Index");
        }
    }
}