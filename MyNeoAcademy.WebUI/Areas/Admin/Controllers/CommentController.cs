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
            await LoadBlogsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadBlogsAsync();
                return View(dto);
            }

            var result = await _commentApiService.CreateAdminCommentAsync(dto); // Burada admin methodu çağrıldı
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yorum eklenemedi.");
            await LoadBlogsAsync();
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

            await LoadBlogsAsync();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCommentWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadBlogsAsync();
                return View(dto);
            }

            var result = await _commentApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yorum güncellenemedi.");
            await LoadBlogsAsync();
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commentApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }

        private async Task LoadBlogsAsync()
        {
            ViewBag.Blogs = await _commentApiService.GetBlogDropdownItemsAsync();
        }
    }
}

