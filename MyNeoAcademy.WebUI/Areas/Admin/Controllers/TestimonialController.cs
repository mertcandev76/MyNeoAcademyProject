using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TestimonialController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("MyApiClient");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // 🔹 Listeleme
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("testimonials");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultTestimonialDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultTestimonialDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"testimonials/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var testimonial = JsonSerializer.Deserialize<ResultTestimonialDTO>(jsonData, _jsonOptions);

            return View(testimonial);
        }

        // 🔹 Ekleme (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.FullName ?? ""), "FullName" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Content ?? ""), "Content" },
                { new StringContent(dto.Rating.ToString()), "Rating" }
            };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PostAsync("testimonials", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Testimonial could not be created.");
            return View(dto);
        }

        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"testimonials/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var resultTestimonial = JsonSerializer.Deserialize<ResultTestimonialDTO>(jsonData, _jsonOptions);

            if (resultTestimonial == null)
                return RedirectToAction("Index");

            var dto = new UpdateTestimonialWithFileDTO
            {
                TestimonialID = resultTestimonial.TestimonialID,
                FullName = resultTestimonial.FullName,
                Title = resultTestimonial.Title,
                Content = resultTestimonial.Content,
                Rating = resultTestimonial.Rating,
                ImageUrl = resultTestimonial.ImageUrl
            };

            return View(dto);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTestimonialWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.TestimonialID.ToString()), "TestimonialID" },
                { new StringContent(dto.FullName ?? ""), "FullName" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Content ?? ""), "Content" },
                { new StringContent(dto.Rating.ToString()), "Rating" }
            };

            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PutAsync("testimonials", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Testimonial could not be updated.");
            return View(dto);
        }

        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"testimonials/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Testimonial could not be deleted.";
            return RedirectToAction("Index");
        }
    }
}
