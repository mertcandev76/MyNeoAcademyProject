using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
