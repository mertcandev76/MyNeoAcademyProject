using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using MyNeoAcademy.WebUI.Helpers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseController : Controller
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public CourseController(IHttpClientFactory httpClientFactory)
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
            var response = await _client.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultCourseDTO>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ResultCourseDTO>>(jsonData, _jsonOptions);

            return View(data);
        }

        // 🔹 Detay
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"courses/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<ResultCourseDTO>(jsonData, _jsonOptions);

            return View(course);
        }

        // 🔹 Ekleme (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await DropdownHelper.GetDropdownItemsAsync<ResultCategoryDTO>(
           _client,
           "categories",
           c => c.Name!,
           c => c.CategoryID.ToString());

            ViewBag.Categories = categories ?? new List<SelectListItem>();

            var instructors = await DropdownHelper.GetDropdownItemsAsync<ResultInstructorDTO>(
                _client,
                "instructors",
                i => i.FullName,
                i => i.InstructorID.ToString());

            ViewBag.Instructors = instructors ?? new List<SelectListItem>();

            return View();
        }

        // 🔹 Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Description ?? ""), "Description" },
                { new StringContent(dto.Rating.ToString()), "Rating" },
                { new StringContent(dto.ReviewCount.ToString()), "ReviewCount" },
                { new StringContent(dto.StudentCount.ToString()), "StudentCount" },
                { new StringContent(dto.LikeCount.ToString()), "LikeCount" },
                { new StringContent(dto.Price?.ToString() ?? "0"), "Price" },
             { new StringContent(dto.CategoryID?.ToString() ?? ""), "CategoryID" },
             { new StringContent(dto.InstructorID?.ToString() ?? ""), "InstructorID" },
            };


            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PostAsync("courses", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Course could not be created.");

            // Dropdownları tekrar yükle
            ViewBag.Categories = await DropdownHelper.GetDropdownItemsAsync<ResultCategoryDTO>(
                _client, "categories", c => c.Name!, c => c.CategoryID.ToString());

            ViewBag.Instructors = await DropdownHelper.GetDropdownItemsAsync<ResultInstructorDTO>(
                _client, "instructors", i => i.FullName, i => i.InstructorID.ToString());

            return View(dto);
        }

        // 🔹 Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"courses/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var jsonData = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<ResultCourseDTO>(jsonData, _jsonOptions);

            if (course == null)
                return RedirectToAction("Index");

            var dto = new UpdateCourseWithFileDTO
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Description = course.Description,
                Rating = course.Rating,
                ReviewCount = course.ReviewCount,
                StudentCount = course.StudentCount,
                LikeCount = course.LikeCount,
                Price = course.Price,
                CategoryID = course.CategoryID,
                InstructorID = course.InstructorID,
                ImageUrl = course.ImageUrl
            };

            ViewBag.Categories = await DropdownHelper.GetDropdownItemsAsync<ResultCategoryDTO>(
                _client, "categories", c => c.Name!, c => c.CategoryID.ToString());

            ViewBag.Instructors = await DropdownHelper.GetDropdownItemsAsync<ResultInstructorDTO>(
                _client, "instructors", i => i.FullName, i => i.InstructorID.ToString());

            return View(dto);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCourseWithFileDTO dto)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(dto.CourseID.ToString()), "CourseID" },
                { new StringContent(dto.Title ?? ""), "Title" },
                { new StringContent(dto.Description ?? ""), "Description" },
                { new StringContent(dto.Rating.ToString()), "Rating" },
                { new StringContent(dto.ReviewCount.ToString()), "ReviewCount" },
                { new StringContent(dto.StudentCount.ToString()), "StudentCount" },
                { new StringContent(dto.LikeCount.ToString()), "LikeCount" },
                { new StringContent(dto.Price?.ToString() ?? "0"), "Price" },
                { new StringContent(dto.CategoryID?.ToString() ?? ""), "CategoryID" },
                { new StringContent(dto.InstructorID?.ToString() ?? ""), "InstructorID" },
            };


            if (dto.ImageFile != null)
            {
                var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
            }

            var response = await _client.PutAsync("courses", formData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Course could not be updated.");

            // Dropdownları tekrar yükle
            ViewBag.Categories = await DropdownHelper.GetDropdownItemsAsync<ResultCategoryDTO>(
                _client, "categories", c => c.Name!, c => c.CategoryID.ToString());

            ViewBag.Instructors = await DropdownHelper.GetDropdownItemsAsync<ResultInstructorDTO>(
                _client, "instructors", i => i.FullName, i => i.InstructorID.ToString());

            return View(dto);
        }

        // 🔹 Silme
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"courses/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Course could not be deleted.";
            return RedirectToAction("Index");
        }
    }
}

