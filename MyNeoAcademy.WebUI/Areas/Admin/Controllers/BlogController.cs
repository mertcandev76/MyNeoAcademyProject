using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        private readonly ICategoryApiService _categoryApiService;
        private readonly IAuthorApiService _authorApiService;

        public BlogController(
            IBlogApiService blogApiService,
            ICategoryApiService categoryApiService,
            IAuthorApiService authorApiService)
        {
            _blogApiService = blogApiService;
            _categoryApiService = categoryApiService;
            _authorApiService = authorApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _blogApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _blogApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(dto);
            }

            var result = await _blogApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Blog eklenemedi.");
            await LoadDropdownsAsync();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _blogApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateBlogWithFileDTO
            {
                BlogID = result.BlogID,
                Title = result.Title,
                ShortDescription = result.ShortDescription,
                Content = result.Content,
                ImageUrl = result.ImageUrl,
                CategoryID = result.Category?.CategoryID,
                AuthorID = result.Author?.AuthorID
            };

            await LoadDropdownsAsync();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(dto);
            }

            var result = await _blogApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Blog güncellenemedi.");
            await LoadDropdownsAsync();
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _blogApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
        private async Task LoadDropdownsAsync()
        {
            ViewBag.Categories = await _categoryApiService.GetDropdownItemsAsync();
            ViewBag.Authors = await _authorApiService.GetDropdownItemsAsync();
        }
    }
}
