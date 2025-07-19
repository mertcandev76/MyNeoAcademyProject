using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
