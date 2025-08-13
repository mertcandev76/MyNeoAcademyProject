using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.Areas.Instructor.Models
{
    public class MyCoursesCommentsViewModel
    {
        public List<ResultCourseDTO> Courses { get; set; } = new();
        public List<ResultCommentDTO> Comments { get; set; } = new();
        public int? SelectedCourseId { get; set; }
    }
}
