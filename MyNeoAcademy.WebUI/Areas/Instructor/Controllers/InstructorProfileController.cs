using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Data;
using System.Security.Claims;


namespace MyNeoAcademy.WebUI.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class InstructorProfileController : Controller
    {
        private readonly IInstructorApiService _instructorApiService;
        private readonly ICourseApiService _courseApiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InstructorProfileController(
            IInstructorApiService instructorApiService,
            ICourseApiService courseApiService,
            IHttpContextAccessor httpContextAccessor)
        {
            _instructorApiService = instructorApiService;
            _courseApiService = courseApiService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentAppUserId()
            => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private async Task LoadDropdownsAsync()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out var appUserId))
            {
                ViewBag.Courses = new List<SelectListItem>();
                return;
            }

            var instructor = await _instructorApiService.GetByAppUserIdAsync(appUserId);
            if (instructor == null)
            {
                ViewBag.Courses = new List<SelectListItem>();
                return;
            }

            // Burada instructor'a ait kursları alıyoruz
            var courses = await _courseApiService.GetCoursesByInstructorIdAsync(instructor.InstructorID);

            ViewBag.Courses = courses.Select(c => new SelectListItem
            {
                Text = c.Title ?? "Başlıksız Kurs",
                Value = c.CourseID.ToString()
            }).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var appUserId = GetCurrentAppUserId();
            var instructor = await _instructorApiService.GetByAppUserIdAsync(appUserId);

            if (instructor == null)
            {
                TempData["Error"] = "Profil bulunamadı.";
                return RedirectToAction("Index", "Dashboard", new { area = "Instructor" });
            }

            var model = new UpdateInstructorWithFileDTO
            {
                InstructorID = instructor.InstructorID,
                FullName = instructor.FullName,
                Title = instructor.Title,
                Bio = instructor.Bio,
                FacebookUrl = instructor.FacebookUrl,
                TwitterUrl = instructor.TwitterUrl,
                WebsiteUrl = instructor.WebsiteUrl,
                AppUserID = instructor.AppUserID,
                ImageUrl = instructor.ImageUrl
            };

            await LoadDropdownsAsync(); // Dropdownlar yüklensin

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateInstructorWithFileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(); // Dropdownlar validasyon hatalarında da dolsun
                return View(dto);
            }

            var success = await _instructorApiService.UpdateAsync(dto);
            if (!success)
            {
                TempData["Error"] = "Profil güncellenemedi.";
                await LoadDropdownsAsync();
                return View(dto);
            }

            TempData["Success"] = "Profil başarıyla güncellendi.";
            return RedirectToAction("Index", "Dashboard", new { area = "Instructor" });
        }
    }
}



//namespace MyNeoAcademy.WebUI.Areas.Instructor.Controllers
//{
//    [Area("Instructor")]
//    [Authorize(Roles = "Instructor")]
//    public class InstructorProfileController : Controller
//    {
//        private readonly IInstructorApiService _instructorApiService;

//        public InstructorProfileController(IInstructorApiService instructorApiService)
//        {
//            _instructorApiService = instructorApiService;
//        }

//        private int GetCurrentAppUserId()
//        {
//            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Edit()
//        {
//            var appUserId = GetCurrentAppUserId();
//            var instructor = await _instructorApiService.GetByAppUserIdAsync(appUserId);

//            if (instructor == null)
//            {
//                TempData["Error"] = "Profil bulunamadı.";
//                return RedirectToAction("Index", "Dashboard", new { area = "Instructor" });
//            }

//            var model = new UpdateInstructorWithFileDTO
//            {
//                InstructorID = instructor.InstructorID,
//                FullName = instructor.FullName,
//                Title = instructor.Title,
//                Bio = instructor.Bio,
//                FacebookUrl = instructor.FacebookUrl,
//                TwitterUrl = instructor.TwitterUrl,
//                WebsiteUrl = instructor.WebsiteUrl,
//                AppUserID = instructor.AppUserID,
//                ImageUrl = instructor.ImageUrl
//            };

//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(UpdateInstructorWithFileDTO dto)
//        {
//            if (!ModelState.IsValid)
//                return View(dto);

//            var success = await _instructorApiService.UpdateAsync(dto);
//            if (!success)
//            {
//                TempData["Error"] = "Profil güncellenemedi.";
//                return View(dto);
//            }

//            TempData["Success"] = "Profil başarıyla güncellendi.";
//            return RedirectToAction("Index", "Dashboard", new { area = "Instructor" });
//        }
//    }
//}
