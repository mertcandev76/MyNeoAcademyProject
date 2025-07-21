using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult Contact()
        {
            return View();
        }
    }
}