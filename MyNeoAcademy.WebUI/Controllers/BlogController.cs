using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.Controllers
{
    public class BlogController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
