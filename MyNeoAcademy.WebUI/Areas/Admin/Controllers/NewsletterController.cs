using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.DTO.DTOs;
using System.Text.Json;
using System.Text;
using Microsoft.SqlServer.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using MyNeoAcademy.Entity.Entities;
using System.Xml.Linq;

[Area("Admin")]
[Route("[area]/[controller]/[action]/{id?}")]
public class NewsletterController : Controller
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public NewsletterController(IHttpClientFactory httpClientFactory)
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
        var response = await _client.GetAsync("newsletters");

        if (!response.IsSuccessStatusCode)
            return View(new List<ResultNewsletterDTO>());

        var jsonData = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<ResultNewsletterDTO>>(jsonData, _jsonOptions);

        return View(data);
    }

    // 🔹 Detay
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var response = await _client.GetAsync($"newsletters/{id}");

        if (!response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        var jsonData = await response.Content.ReadAsStringAsync();
        var newsletter = JsonSerializer.Deserialize<ResultNewsletterDTO>(jsonData, _jsonOptions);

        return View(newsletter);
    }

    // 🔹 Ekleme (GET)
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // 🔹 Ekleme (POST)
    [HttpPost]
    public async Task<IActionResult> Create(CreateNewsletterDTO dto)
    {

        var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("newsletters", jsonContent);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError("", "Newsletter could not be created.");
        return View(dto);
    }

    // 🔹 Güncelleme (GET)
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _client.GetAsync($"newsletters/{id}");

        if (!response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        var jsonData = await response.Content.ReadAsStringAsync();
        var resultNewsletter = JsonSerializer.Deserialize<ResultNewsletterDTO>(jsonData, _jsonOptions);

        if (resultNewsletter == null)
            return RedirectToAction("Index");

        var dto = new UpdateNewsletterDTO
        {
            NewsletterID = resultNewsletter.NewsletterID,
            Email = resultNewsletter.Email,

        };

        return View(dto);
    }

    // 🔹 Güncelleme (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateNewsletterDTO dto)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("newsletters", jsonContent);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError("", "Newsletter could not be updated.");
        return View(dto);
    }

    // 🔹 Silme
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _client.DeleteAsync($"newsletters/{id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        TempData["Error"] = "Newsletter could not be deleted.";
        return RedirectToAction("Index");
    }
}
