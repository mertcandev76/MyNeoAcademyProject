using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;

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
