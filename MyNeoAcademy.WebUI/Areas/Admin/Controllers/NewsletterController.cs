using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;
using System.Text;
using Microsoft.SqlServer.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using MyNeoAcademy.Entity.Entities;
using System.Xml.Linq;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class NewsletterController : Controller
    {
        private readonly INewsletterApiService _newsletterApiService;

        public NewsletterController(INewsletterApiService newsletterApiService)
        {
            _newsletterApiService = newsletterApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _newsletterApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _newsletterApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsletterDTO dto)
        {
            var result = await _newsletterApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Abonelik oluşturulamadı.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _newsletterApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateNewsletterDTO
            {
                NewsletterID = result.NewsletterID,
                Email = result.Email
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateNewsletterDTO dto)
        {
            var result = await _newsletterApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Abonelik güncellenemedi.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _newsletterApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }

}
