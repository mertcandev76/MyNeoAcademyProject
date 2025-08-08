    using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using MyNeoAcademy.WebUI.ApiServices.Concrete;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseApiService _courseApiService;

        public CourseController(
            ICourseApiService courseApiService)
        {
            _courseApiService = courseApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _courseApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _courseApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _courseApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}

