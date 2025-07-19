using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.DTO.DTOs;
using AutoMapper;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.Entity.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;
using System.Text.Json;
using MyNeoAcademy.WebUI.Helpers;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SliderController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public SliderController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");

            // JSON ayarları (büyük-küçük harf duyarsız deserialization)
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // 🔹 Listeleme
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("sliders");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultSliderDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultSliderDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"sliders/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var slider = JsonSerializer.Deserialize<ResultSliderDTO>(jsonData, _jsonOptions);

            return View(slider);
        }

        // 🔹 Ekleme (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.SubTitle ?? ""), "SubTitle" },
                { new StringContent(dto.ButtonText ?? ""), "ButtonText" },
                { new StringContent(dto.ButtonUrl ?? ""), "ButtonUrl" }
            };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PostAsync("sliders", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Slider eklenemedi.");
            return View(dto);
        }

        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"sliders/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var resultSlider = JsonSerializer.Deserialize<ResultSliderDTO>(jsonData, _jsonOptions);

            if (resultSlider == null)
                return RedirectToAction("Index");
            var dto = new UpdateSliderWithFileDTO
            {
                SliderID = resultSlider.SliderID,
                Title = resultSlider.Title,
                SubTitle = resultSlider.SubTitle,
                ButtonText = resultSlider.ButtonText,
                ButtonUrl = resultSlider.ButtonUrl,
                ImageUrl = resultSlider.ImageUrl
            };

            return View(dto);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSliderWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.SliderID.ToString()), "SliderID" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.SubTitle ?? ""), "SubTitle" },
                { new StringContent(dto.ButtonText ?? ""), "ButtonText" },
                { new StringContent(dto.ButtonUrl ?? ""), "ButtonUrl" }
            };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PutAsync("sliders", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Slider güncellenemedi.");
            return View(dto);
        }

        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"sliders/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Slider silinemedi.";
            return RedirectToAction("Index");
        }
    }
}
