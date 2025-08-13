using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.ApiServices.Concrete;
using System.Data;
using System.Security.Claims;



namespace MyNeoAcademy.WebUI.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Route("Instructor/[controller]/[action]")]
    [Authorize(Roles = "Instructor")]
    public class CourseLikeController : Controller
    {
        private readonly ICourseLikeApiService _courseLikeApiService;
        private readonly ICourseApiService _courseApiService;
        private readonly IAppUserApiService _appUserApiService;
        private readonly IInstructorApiService _instructorApiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseLikeController(
            ICourseLikeApiService courseLikeApiService,
            ICourseApiService courseApiService,
            IAppUserApiService appUserApiService,
            IInstructorApiService instructorApiService,
            IHttpContextAccessor httpContextAccessor)
        {
            _courseLikeApiService = courseLikeApiService;
            _courseApiService = courseApiService;
            _appUserApiService = appUserApiService;
            _instructorApiService = instructorApiService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int? GetCurrentAppUserId()
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdStr, out var appUserId))
                return appUserId;
            return null;
        }

        private async Task<int?> GetInstructorIdForCurrentUserAsync()
        {
            var appUserId = GetCurrentAppUserId();
            if (appUserId == null)
                return null;

            var instructor = await _instructorApiService.GetByAppUserIdAsync(appUserId.Value);
            return instructor?.InstructorID;
        }

        public async Task<IActionResult> Index()
        {
            // API tarafında token'dan instructorId alınıyor
            var likes = await _courseLikeApiService.GetByInstructorIdAsync();
            return View(likes);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var like = await _courseLikeApiService.GetByIdAsync(id);
            if (like == null)
                return NotFound();

            return View(like);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseLikeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(dto);
            }

            var success = await _courseLikeApiService.CreateAsync(dto);
            if (!success)
            {
                ModelState.AddModelError("", "Beğeni eklenirken bir hata oluştu.");
                await LoadDropdownsAsync();
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _courseLikeApiService.DeleteAsync(id);
            if (!success)
            {
                TempData["Error"] = "Beğeni silme işlemi başarısız oldu.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdownsAsync()
        {
            var instructorId = await GetInstructorIdForCurrentUserAsync();
            if (instructorId == null)
            {
                ViewBag.Courses = new List<SelectListItem>();
                ViewBag.Users = new List<SelectListItem>();
                return;
            }

            var courses = await _courseApiService.GetCoursesByInstructorIdAsync(instructorId.Value);
            var users = await _appUserApiService.GetAllAsync(); // Kullanıcıları filtrelemek istersen ayrıca değiştirilebilir

            ViewBag.Courses = courses.Select(c => new SelectListItem
            {
                Text = c.Title ?? "Başlıksız Kurs",
                Value = c.CourseID.ToString()
            }).ToList();

            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Text = u.FullName ?? "İsimsiz Kullanıcı",
                Value = u.Id.ToString()
            }).ToList();
        }
    }
}

//namespace MyNeoAcademy.WebUI.Areas.Instructor.Controllers
//{
//    [Area("Instructor")]
//    [Route("Instructor/[controller]/[action]")]
//    [Authorize(Roles = "Instructor")]
//    public class CourseLikeController : Controller
//    {
//        private readonly ICourseLikeApiService _courseLikeApiService;
//        private readonly ICourseApiService _courseApiService;
//        private readonly IAppUserApiService _appUserApiService;

//        public CourseLikeController(
//            ICourseLikeApiService courseLikeApiService,
//            ICourseApiService courseApiService,
//            IAppUserApiService appUserApiService)
//        {
//            _courseLikeApiService = courseLikeApiService;
//            _courseApiService = courseApiService;
//            _appUserApiService = appUserApiService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            // API tarafında token'dan instructorId alınıyor
//            var likes = await _courseLikeApiService.GetByInstructorIdAsync();
//            return View(likes);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Detail(int id)
//        {
//            var like = await _courseLikeApiService.GetByIdAsync(id);
//            if (like == null)
//                return NotFound();

//            return View(like);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Create()
//        {
//            await LoadDropdownsAsync();
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(CreateCourseLikeDTO dto)
//        {
//            if (!ModelState.IsValid)
//            {
//                await LoadDropdownsAsync();
//                return View(dto);
//            }

//            var success = await _courseLikeApiService.CreateAsync(dto);
//            if (!success)
//            {
//                ModelState.AddModelError("", "Beğeni eklenirken bir hata oluştu.");
//                await LoadDropdownsAsync();
//                return View(dto);
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        [HttpGet]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var success = await _courseLikeApiService.DeleteAsync(id);
//            if (!success)
//            {
//                TempData["Error"] = "Beğeni silme işlemi başarısız oldu.";
//            }
//            return RedirectToAction(nameof(Index));
//        }

//        private async Task LoadDropdownsAsync()
//        {
//            var courses = await _courseApiService.GetAllAsync();
//            var users = await _appUserApiService.GetAllAsync();

//            ViewBag.Courses = courses.Select(c => new SelectListItem
//            {
//                Text = c.Title ?? "Başlıksız Kurs",
//                Value = c.CourseID.ToString()
//            }).ToList();

//            ViewBag.Users = users.Select(u => new SelectListItem
//            {
//                Text = u.FullName ?? "İsimsiz Kullanıcı",
//                Value = u.Id.ToString()
//            }).ToList();
//        }
//    }
//}
