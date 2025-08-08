using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Data;
using System.Security.Claims;

namespace MyNeoAcademy.WebUI.Areas.Instructor.Controllers
{
    [Authorize(Roles = "Instructor")]
    [Area("Instructor")]
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

        private int GetCurrentAppUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        private async Task<int?> GetInstructorIdForCurrentUserAsync()
        {
            var appUserId = GetCurrentAppUserId();
            var instructor = await _instructorApiService.GetByAppUserIdAsync(appUserId);
            return instructor?.InstructorID;
        }

        private async Task LoadDropdownsAsync()
        {
            ViewBag.Categories = await _categoryApiService.GetDropdownItemsAsync();
        }

        public async Task<IActionResult> MyCourses()
        {
            var allCourses = await _courseApiService.GetAllAsync();
            var instructorId = await GetInstructorIdForCurrentUserAsync();
            if (instructorId == null)
                return RedirectToAction("AccessDenied", "Account"); 

            var myCourses = allCourses.Where(c => c.Instructor?.InstructorID == instructorId).ToList();
            return View(myCourses);
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

            var instructorId = await GetInstructorIdForCurrentUserAsync();
            if (instructorId == null)
            {
                ModelState.AddModelError("", "Eğitmen bulunamadı.");
                await LoadDropdownsAsync();
                return View(dto);
            }

            dto.InstructorID = instructorId.Value;

            var result = await _courseApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("MyCourses");

            ModelState.AddModelError("", "Kurs oluşturulamadı.");
            await LoadDropdownsAsync();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseApiService.GetByIdAsync(id);
            var instructorId = await GetInstructorIdForCurrentUserAsync();

            if (course == null || instructorId == null || course.Instructor?.InstructorID != instructorId)
                return RedirectToAction("MyCourses");

            var dto = new UpdateCourseWithFileDTO
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Description = course.Description,
                ImageUrl = course.ImageUrl,
                Rating = course.Rating,
                ReviewCount = course.ReviewCount,
                StudentCount = course.StudentCount,
                LikeCount = course.LikeCount,
                Price = course.Price,
                CategoryID = course.Category?.CategoryID,
                InstructorID = course.Instructor?.InstructorID
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

            var instructorId = await GetInstructorIdForCurrentUserAsync();
            if (instructorId == null)
            {
                ModelState.AddModelError("", "Eğitmen bulunamadı.");
                await LoadDropdownsAsync();
                return View(dto);
            }

            dto.InstructorID = instructorId.Value;

            var result = await _courseApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("MyCourses");

            ModelState.AddModelError("", "Kurs güncellenemedi.");
            await LoadDropdownsAsync();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseApiService.GetByIdAsync(id);
            var instructorId = await GetInstructorIdForCurrentUserAsync();

            if (course == null || instructorId == null || course.Instructor?.InstructorID != instructorId)
                return RedirectToAction("MyCourses");

            var result = await _courseApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Kurs silinemedi.";

            return RedirectToAction("MyCourses");
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var result = await _courseApiService.DeleteAsync(id);
        //    if (!result)
        //        TempData["Error"] = "Silme işlemi başarısız.";

        //    return RedirectToAction("MyCourses");
        //}
    }
}

