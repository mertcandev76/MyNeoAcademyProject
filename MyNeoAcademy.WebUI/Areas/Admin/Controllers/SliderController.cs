using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using AutoMapper;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.Entity.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SliderController : Controller
    {
        private readonly HttpClient _httpClient;

        public SliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
            // veya doğrudan base address verebilirsin
        }

        // Listeleme
        public async Task<IActionResult> Index()
        {
            var sliders = await _httpClient.GetFromJsonAsync<List<ResultSliderDTO>>("sliders");
            return View(sliders);
        }

        // Yeni slider oluşturma sayfası
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni slider oluşturma POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderWithFileDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.Title ?? ""), "Title");
            content.Add(new StringContent(dto.SubTitle ?? ""), "SubTitle");
            content.Add(new StringContent(dto.ButtonText ?? ""), "ButtonText");
            content.Add(new StringContent(dto.ButtonUrl ?? ""), "ButtonUrl");

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                content.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _httpClient.PostAsync("sliders", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", error);
            return View(dto);
        }

        // Düzenleme GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _httpClient.GetFromJsonAsync<UpdateSliderWithFileDTO>($"sliders/{id}");
            if (slider == null)
                return NotFound();

            return View(slider);
        }

        // Düzenleme POST
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSliderWithFileDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.SliderID.ToString()), "SliderID");
            content.Add(new StringContent(dto.Title ?? ""), "Title");
            content.Add(new StringContent(dto.SubTitle ?? ""), "SubTitle");
            content.Add(new StringContent(dto.ButtonText ?? ""), "ButtonText");
            content.Add(new StringContent(dto.ButtonUrl ?? ""), "ButtonUrl");

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                content.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _httpClient.PutAsync("sliders", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", error);
            return View(dto);
        }

        // Silme
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"sliders/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["Error"] = error;
            }

            return RedirectToAction("Index");
        }
    }
}
