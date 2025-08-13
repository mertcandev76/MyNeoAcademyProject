using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.Areas.Instructor.Models;
using System.Data;
using System.Security.Claims;



namespace MyNeoAcademy.WebUI.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class CommentController : Controller
    {
        private readonly ICommentApiService _commentApiService;
        private readonly ICourseApiService _courseApiService;
        private readonly IInstructorApiService _instructorApiService;

        public CommentController(
            ICommentApiService commentApiService,
            ICourseApiService courseApiService,
            IInstructorApiService instructorApiService)
        {
            _commentApiService = commentApiService;
            _courseApiService = courseApiService;
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

        // Eğitmenin kurslarının yorumlarını listeleyen genel action
        [HttpGet]
        public async Task<IActionResult> MyCoursesComments(int? courseId)
        {
            var instructorId = await GetInstructorIdForCurrentUserAsync();
            if (instructorId == null)
                return RedirectToAction("AccessDenied", "Login", new { area = "Auth" });

            // Eğitmenin kurslarını al
            var allCourses = await _courseApiService.GetAllAsync();
            var myCourses = allCourses.Where(c => c.Instructor?.InstructorID == instructorId).ToList();

            List<ResultCommentDTO> comments;

            if (courseId.HasValue)
            {
                // Sadece seçilen kursun yorumlarını getir
                comments = await _commentApiService.GetAllCourseCommentsAsync(courseId.Value);
            }
            else
            {
                // Tüm kursların yorumlarını topla
                comments = new List<ResultCommentDTO>();

                foreach (var course in myCourses)
                {
                    var courseComments = await _commentApiService.GetAllCourseCommentsAsync(course.CourseID);
                    comments.AddRange(courseComments);
                }

                // Tarihe göre sırala (son yorumlar önce)
                comments = comments.OrderByDescending(c => c.CreatedDate).ToList();
            }

            var vm = new MyCoursesCommentsViewModel
            {
                Courses = myCourses,
                Comments = comments,
                SelectedCourseId = courseId
            };

            return View(vm);
        }
    }
}




