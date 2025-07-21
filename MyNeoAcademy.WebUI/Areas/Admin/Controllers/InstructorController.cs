using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MyNeoAcademy.Application.DTOs;
using System.Text.Json;

    namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
    {
        [Area("Admin")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public class InstructorController : Controller
        {
            private readonly HttpClient _client;
            private readonly JsonSerializerOptions _jsonOptions;

            public InstructorController(IHttpClientFactory httpClientFactory)
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
                var response = await _client.GetAsync("instructors");

                if (!response.IsSuccessStatusCode)
                    return View(new List<ResultInstructorDTO>());

                var jsonData = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<ResultInstructorDTO>>(jsonData, _jsonOptions);

                return View(data);
            }

            // 🔹 Detay
            [HttpGet]
            public async Task<IActionResult> Details(int id)
            {
                var response = await _client.GetAsync($"instructors/{id}");

                if (!response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                var jsonData = await response.Content.ReadAsStringAsync();
                var instructor = JsonSerializer.Deserialize<ResultInstructorDTO>(jsonData, _jsonOptions);

                return View(instructor);
            }

            // 🔹 Ekleme (GET)
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            // 🔹 Ekleme (POST)
            [HttpPost]
            public async Task<IActionResult> Create(CreateInstructorWithFileDTO dto)
            {
                var formData = new MultipartFormDataContent
                {
                    { new StringContent(dto.FullName ?? ""), "FullName" },
                    { new StringContent(dto.Title ?? ""), "Title" },
                    { new StringContent(dto.Bio ?? ""), "Bio" },
                    { new StringContent(dto.FacebookUrl ?? ""), "FacebookUrl" },
                    { new StringContent(dto.TwitterUrl ?? ""), "TwitterUrl" },
                    { new StringContent(dto.WebsiteUrl ?? ""), "WebsiteUrl" }
                };

                if (dto.ImageFile != null)
                {
                    var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                    formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
                }

                var response = await _client.PostAsync("instructors", formData);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Eğitmen eklenemedi.");
                return View(dto);
            }

            // 🔹 Güncelleme (GET)
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var response = await _client.GetAsync($"instructors/{id}");

                if (!response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                var jsonData = await response.Content.ReadAsStringAsync();
                var resultInstructor = JsonSerializer.Deserialize<ResultInstructorDTO>(jsonData, _jsonOptions);

                if (resultInstructor == null)
                    return RedirectToAction("Index");

                var dto = new UpdateInstructorWithFileDTO
                {
                    InstructorID = resultInstructor.InstructorID,
                    FullName = resultInstructor.FullName,
                    Title = resultInstructor.Title,
                    Bio = resultInstructor.Bio,
                    FacebookUrl = resultInstructor.FacebookUrl,
                    TwitterUrl = resultInstructor.TwitterUrl,
                    WebsiteUrl = resultInstructor.WebsiteUrl,
                    ImageUrl = resultInstructor.ImageUrl
                };

                return View(dto);
            }

            // 🔹 Güncelleme (POST)
            [HttpPost]
            public async Task<IActionResult> Edit(UpdateInstructorWithFileDTO dto)
            {
                var formData = new MultipartFormDataContent
                {
                    { new StringContent(dto.InstructorID.ToString()), "InstructorID" },
                    { new StringContent(dto.FullName ?? ""), "FullName" },
                    { new StringContent(dto.Title ?? ""), "Title" },
                    { new StringContent(dto.Bio ?? ""), "Bio" },
                    { new StringContent(dto.FacebookUrl ?? ""), "FacebookUrl" },
                    { new StringContent(dto.TwitterUrl ?? ""), "TwitterUrl" },
                    { new StringContent(dto.WebsiteUrl ?? ""), "WebsiteUrl" }
                };

                if (dto.ImageFile != null)
                {
                    var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.ImageFile.ContentType);
                    formData.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
                }

                var response = await _client.PutAsync("instructors", formData);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Eğitmen güncellenemedi.");
                return View(dto);
            }

            // 🔹 Silme
            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {
                var response = await _client.DeleteAsync($"instructors/{id}");

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                TempData["Error"] = "Eğitmen silinemedi.";
                return RedirectToAction("Index");
            }
        }
    }
