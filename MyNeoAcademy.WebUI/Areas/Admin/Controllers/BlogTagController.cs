using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogTagController : Controller
    {
        private readonly IBlogTagApiService _blogTagApiService;


        private readonly IBlogApiService _blogApiService;
        private readonly ITagApiService _tagApiService;

        public BlogTagController(
            IBlogTagApiService blogTagApiService,
            IBlogApiService blogApiService,
            ITagApiService tagApiService)
        {
            _blogTagApiService = blogTagApiService;
            _blogApiService = blogApiService;
            _tagApiService = tagApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _blogTagApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _blogTagApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Blogs = await _blogApiService.GetDropdownItemsAsync();
            ViewBag.Tags = await _tagApiService.GetDropdownItemsAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogTagDTO dto)
        {
            var result = await _blogTagApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Blog-Etiket eklenemedi.");


            ViewBag.Blogs = await _blogApiService.GetDropdownItemsAsync();
            ViewBag.Tags = await _tagApiService.GetDropdownItemsAsync();

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _blogTagApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateBlogTagDTO
            {
                BlogTagID = result.BlogTagID,
                BlogID = result.Blog.BlogID,
                TagID = result.Tag.TagID
            };

            ViewBag.Blogs = await _blogApiService.GetDropdownItemsAsync();
            ViewBag.Tags = await _tagApiService.GetDropdownItemsAsync();

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogTagDTO dto)
        {
            var result = await _blogTagApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Blog-Etiket güncellenemedi.");

            ViewBag.Blogs = await _blogApiService.GetDropdownItemsAsync();
            ViewBag.Tags = await _tagApiService.GetDropdownItemsAsync();

            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _blogTagApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}
