using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using System.Text;
using MyNeoAcademy.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CommentController : Controller
    {
        private readonly ICommentApiService _commentApiService;

        public CommentController(ICommentApiService commentApiService)
        {
            _commentApiService = commentApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _commentApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _commentApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Blogs = await _commentApiService.GetBlogDropdownItemsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Blogs = await _commentApiService.GetBlogDropdownItemsAsync();
                return View(dto);
            }

            var result = await _commentApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yorum eklenemedi.");
            ViewBag.Blogs = await _commentApiService.GetBlogDropdownItemsAsync();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _commentApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateCommentWithFileDTO
            {
                CommentID = result.CommentID,
                UserName = result.UserName,
                Email = result.Email,
                Content = result.Content,
                ImageUrl = result.ImageUrl,
                BlogID = result.Blog?.BlogID ?? 0
            };

            ViewBag.Blogs = await _commentApiService.GetBlogDropdownItemsAsync();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCommentWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Blogs = await _commentApiService.GetBlogDropdownItemsAsync();
                return View(dto);
            }

            var result = await _commentApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yorum güncellenemedi.");
            ViewBag.Blogs = await _commentApiService.GetBlogDropdownItemsAsync();
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commentApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}
