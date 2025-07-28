using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecentBlogPostsController : ControllerBase
    {
        private readonly IRecentBlogPostService _recentBlogPostService;
        private readonly IWebHostEnvironment _env;

        public RecentBlogPostsController(IRecentBlogPostService recentBlogPostService, IWebHostEnvironment env)
        {
            _recentBlogPostService = recentBlogPostService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var posts = await _recentBlogPostService.GetListAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var post = await _recentBlogPostService.GetByIdAsync(id);
                if (post == null)
                    return NotFound("Blog kaydı bulunamadı.");

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CreateRecentBlogPostWithFileDTO dto)
        {
            try
            {
                await _recentBlogPostService.CreateWithFileAsync(dto, _env.WebRootPath);
                return Ok("Yeni blog gönderisi oluşturuldu.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ekleme hatası: {ex.Message}");
            }
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] UpdateRecentBlogPostWithFileDTO dto)
        {
            try
            {
                await _recentBlogPostService.UpdateWithFileAsync(dto, _env.WebRootPath);
                return Ok("Blog gönderisi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Güncelleme hatası: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _recentBlogPostService.DeleteByIdAsync(id);
                if (!deleted)
                    return NotFound("Blog gönderisi bulunamadı.");

                return Ok("Blog gönderisi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Silme hatası: {ex.Message}");
            }
        }
    }
}
