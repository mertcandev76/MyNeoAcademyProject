using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.Areas.Instructor.Models
{
    public class InstructorDashboardViewModel
    {
        public int TotalCourses { get; set; }
        public int TotalStudents { get; set; }
        public int TotalLikes { get; set; }
        public int TotalReviews { get; set; }
        public double AverageRating { get; set; }

        public List<ResultCourseDTO> RecentCourses { get; set; } = new();
        public List<ResultCourseDTO> PopularCourses { get; set; } = new();
    }

}
