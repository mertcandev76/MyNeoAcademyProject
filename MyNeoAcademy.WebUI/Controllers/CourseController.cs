using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
