using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.Areas.Instructor.Models;
using System.Security.Claims;
using System.Data;


namespace MyNeoAcademy.WebUI.Areas.Instructor.Controllers
{
    [Authorize(Roles = "Instructor")]
    [Area("Instructor")]
    public class DashboardController : Controller
    {
        private readonly ICourseApiService _courseApiService;
        private readonly IInstructorApiService _instructorApiService;

        public DashboardController(
            ICourseApiService courseApiService,
            IInstructorApiService instructorApiService)
        {
            _courseApiService = courseApiService;
            _instructorApiService = instructorApiService;
        }

        private int GetCurrentAppUserId()
            => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private async Task<int?> GetInstructorIdForCurrentUserAsync()
        {
            var appUserId = GetCurrentAppUserId();
            var instructor = await _instructorApiService.GetByAppUserIdAsync(appUserId);
            return instructor?.InstructorID;
        }

        public async Task<IActionResult> Index()
        {
            var instructorId = await GetInstructorIdForCurrentUserAsync();
            if (instructorId == null)
                return RedirectToAction("AccessDenied", "Login", new { area = "Auth" });

            // Instructor kurslarını al
            var courses = await _courseApiService.GetCoursesByInstructorIdAsync(instructorId.Value);


            // Dashboard model oluştur
            var model = new InstructorDashboardViewModel
            {
                TotalCourses = courses.Count,
                TotalStudents = courses.Sum(c => c.StudentCount),
                TotalLikes = courses.Sum(c => c.LikeCount),
                AverageRating = courses.Any() ? (int)Math.Round(courses.Average(c => c.Rating)) : 0,
                RecentCourses = courses.OrderByDescending(c => c.CourseID).Take(5).ToList(),
                PopularCourses = courses.OrderByDescending(c => c.LikeCount).Take(5).ToList()
            };

            return View(model);
        }
    }
}