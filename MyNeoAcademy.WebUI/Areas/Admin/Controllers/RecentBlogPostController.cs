using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class RecentBlogPostController : Controller
    {
        private readonly IRecentBlogPostApiService _recentBlogPostApiService;

        public RecentBlogPostController(IRecentBlogPostApiService recentBlogPostApiService)
        {
            _recentBlogPostApiService = recentBlogPostApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _recentBlogPostApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _recentBlogPostApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecentBlogPostWithFileDTO dto)
        {
            var result = await _recentBlogPostApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Blog gönderisi eklenemedi.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _recentBlogPostApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateRecentBlogPostWithFileDTO
            {
                RecentBlogPostID = result.RecentBlogPostID,
                CompactTitle = result.CompactTitle,
                ThumbnailUrl = result.ThumbnailUrl
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRecentBlogPostWithFileDTO dto)
        {
            var result = await _recentBlogPostApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Blog gönderisi güncellenemedi.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _recentBlogPostApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}
