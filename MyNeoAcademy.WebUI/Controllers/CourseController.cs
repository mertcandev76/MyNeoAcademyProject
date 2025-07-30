using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // ViewComponent'i AJAX ile tetiklemek için kullanılan Action
        [HttpGet]
        public IActionResult LoadCoursesByCategory(int categoryId)
        {
            return ViewComponent("CourseList", new { displayType = "CourseMenu", categoryId = categoryId });
        }

    }
}
