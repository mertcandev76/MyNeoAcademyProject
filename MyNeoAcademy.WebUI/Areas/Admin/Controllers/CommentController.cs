using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using System.Text;
using MyNeoAcademy.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using MyNeoAcademy.WebUI.ApiServices.Concrete;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentApiService _commentApiService;
        private readonly IAppUserApiService _appUserApiService;
        private readonly ICourseApiService _courseApiService; 

        public CommentController(
            ICommentApiService commentApiService,
            IAppUserApiService appUserApiService,
            ICourseApiService courseApiService) 
        {
            _commentApiService = commentApiService;
            _appUserApiService = appUserApiService;
            _courseApiService = courseApiService; 
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
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(dto.CourseID);
                return View(dto);
            }

            var result = await _commentApiService.CreateAdminCommentAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yorum eklenemedi.");
            await LoadDropdownsAsync(dto.CourseID);
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
                BlogID = result.Blog?.BlogID ?? 0,
                CourseID = result.Course?.CourseID ?? 0 
            };

            await LoadDropdownsAsync(dto.CourseID);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCommentWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(dto.CourseID);
                return View(dto);
            }

            var result = await _commentApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Yorum güncellenemedi.");
            await LoadDropdownsAsync(dto.CourseID);
            return View(dto);
        }

        private async Task LoadDropdownsAsync(object? selectedCourse = null, object? selectedUser = null, object? selectedBlog = null)
        {
            var appUserList = await _appUserApiService.GetDropdownItemsByRoleAsync("User");
            ViewBag.AppUserList = new SelectList(appUserList, "Value", "Text", selectedUser);

            var blogList = await _commentApiService.GetBlogDropdownItemsAsync();
            ViewBag.Blogs = new SelectList(blogList, "Value", "Text", selectedBlog);

            var courses = await _courseApiService.GetDropdownItemsAsync();
            ViewBag.Courses = new SelectList(courses, "Value", "Text", selectedCourse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _commentApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}


