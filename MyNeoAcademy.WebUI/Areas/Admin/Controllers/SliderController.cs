using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using AutoMapper;
using MyNeoAcademy.Entity.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;
using System.Text.Json;
using MyNeoAcademy.WebUI.Helpers;
using MyNeoAcademy.WebUI.ApiServices.Abstract;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SliderController : Controller
    {
        private readonly ISliderApiService _sliderApiService;

        public SliderController(ISliderApiService sliderApiService)
        {
            _sliderApiService = sliderApiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _sliderApiService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _sliderApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderWithFileDTO dto)
        {
            var result = await _sliderApiService.CreateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Slider eklenemedi.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _sliderApiService.GetByIdAsync(id);
            if (result == null)
                return RedirectToAction("Index");

            var dto = new UpdateSliderWithFileDTO
            {
                SliderID = result.SliderID,
                SubTitle = result.SubTitle,
                Title = result.Title,
                ButtonText = result.ButtonText,
                ButtonUrl = result.ButtonUrl,
                ImageUrl = result.ImageUrl
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSliderWithFileDTO dto)
        {
            var result = await _sliderApiService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Slider güncellenemedi.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sliderApiService.DeleteAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }
    }
}
