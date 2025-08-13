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
    [Route("Instructor/[controller]/[action]")]
    [Authorize(Roles = "Instructor")]
    public class CourseEnrollmentController : Controller
    {
        private readonly ICourseEnrollmentApiService _courseEnrollmentApiService;
        private readonly ICourseApiService _courseApiService;
        private readonly IAppUserApiService _appUserApiService;
        private readonly IInstructorApiService _instructorApiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseEnrollmentController(
            ICourseEnrollmentApiService courseEnrollmentApiService,
            ICourseApiService courseApiService,
            IAppUserApiService appUserApiService,
            IInstructorApiService instructorApiService,
            IHttpContextAccessor httpContextAccessor)
        {
            _courseEnrollmentApiService = courseEnrollmentApiService;
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
            if (appUserId == null) return null;
            var instructor = await _instructorApiService.GetByAppUserIdAsync(appUserId.Value);
            return instructor?.InstructorID;
        }

        public async Task<IActionResult> Index()
        {
            // API zaten token'dan instructor id alıyorsa burası böyle kalabilir
            var enrollments = await _courseEnrollmentApiService.GetByInstructorIdAsync();
            return View(enrollments);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var enrollment = await _courseEnrollmentApiService.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound();

            return View(enrollment);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseEnrollmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(dto);
            }

            await _courseEnrollmentApiService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseEnrollmentApiService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdownsAsync()
        {
            var instructorId = await GetInstructorIdForCurrentUserAsync();
            if (instructorId == null)
            {
                ViewBag.Courses = new List<SelectListItem>();
            }
            else
            {
                var courses = await _courseApiService.GetCoursesByInstructorIdAsync(instructorId.Value);
                ViewBag.Courses = courses.Select(c => new SelectListItem
                {
                    Text = c.Title ?? "Başlıksız Kurs",
                    Value = c.CourseID.ToString()
                }).ToList();
            }

            var users = await _appUserApiService.GetAllAsync();
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
//    public class CourseEnrollmentController : Controller
//    {
//        private readonly ICourseEnrollmentApiService _courseEnrollmentApiService;
//        private readonly ICourseApiService _courseApiService;
//        private readonly IAppUserApiService _appUserApiService;

//        public CourseEnrollmentController(
//            ICourseEnrollmentApiService courseEnrollmentApiService,
//            ICourseApiService courseApiService,
//            IAppUserApiService appUserApiService)
//        {
//            _courseEnrollmentApiService = courseEnrollmentApiService;
//            _courseApiService = courseApiService;
//            _appUserApiService = appUserApiService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            // API zaten instructor id'yi token'dan alıyor
//            var enrollments = await _courseEnrollmentApiService.GetByInstructorIdAsync();
//            return View(enrollments);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Detail(int id)
//        {
//            var enrollment = await _courseEnrollmentApiService.GetByIdAsync(id);
//            if (enrollment == null)
//                return NotFound();

//            return View(enrollment);
//        }



//        [HttpGet]
//        public async Task<IActionResult> Create()
//        {
//            await LoadDropdownsAsync();
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(CreateCourseEnrollmentDTO dto)
//        {
//            if (!ModelState.IsValid)
//            {
//                await LoadDropdownsAsync();
//                return View(dto);
//            }

//            await _courseEnrollmentApiService.CreateAsync(dto);
//            return RedirectToAction(nameof(Index));
//        }

//        [HttpGet]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _courseEnrollmentApiService.DeleteAsync(id);
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
