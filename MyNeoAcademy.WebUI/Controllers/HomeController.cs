using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using MyNeoAcademy.DTO.DTOs.StatisticDTOs;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;
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

    }
}