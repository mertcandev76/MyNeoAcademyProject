    using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseController : Controller
    {
        private readonly ICourseApiService _courseApiService;
        private readonly ICategoryApiService _categoryApiService;
        private readonly IInstructorApiService _instructorApiService;

        public CourseController(
            ICourseApiService courseApiService,
            ICategoryApiService categoryApiService,
            IInstructorApiService instructorApiService)
        {
            _courseApiService = courseApiService;
            _categoryApiService = categoryApiService;
            _instructorApiService = instructorApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _courseApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _courseApiService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create(CreateCourseWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(dto);
            }

            var result = await _courseApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Kurs eklenemedi.");
            await LoadDropdownsAsync();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _courseApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateCourseWithFileDTO
            {
                CourseID = result.CourseID,
                Title = result.Title,
                Description = result.Description,
                ImageUrl = result.ImageUrl,
                Rating = result.Rating,
                ReviewCount = result.ReviewCount,
                StudentCount = result.StudentCount,
                LikeCount = result.LikeCount,
                Price = result.Price,
                CategoryID = result.Category?.CategoryID,
                InstructorID = result.Instructor?.InstructorID
            };

            await LoadDropdownsAsync();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCourseWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(dto);
            }

            var result = await _courseApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Kurs güncellenemedi.");
            await LoadDropdownsAsync();
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }


        private async Task LoadDropdownsAsync()
        {
            ViewBag.Categories = await _categoryApiService.GetDropdownItemsAsync();
            ViewBag.Instructors = await _instructorApiService.GetDropdownItemsAsync();
        }
    }
}

