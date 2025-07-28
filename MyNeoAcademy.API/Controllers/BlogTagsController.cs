using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTagsController : ControllerBase
    {
        private readonly IBlogTagService _blogTagService;

        public BlogTagsController(IBlogTagService blogTagService)
        {
            _blogTagService = blogTagService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var blogTags = await _blogTagService.GetAllWithIncludesAsync();
                return Ok(blogTags);
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
                var blogTag = await _blogTagService.GetByIdWithIncludesAsync(id);
                if (blogTag == null)
                    return NotFound("BlogTag bulunamadı.");

                return Ok(blogTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpGet("exists")]
        public async Task<IActionResult> Exists([FromQuery] int blogId, [FromQuery] int tagId)
        {
            try
            {
                var exists = await _blogTagService.ExistsAsync(blogId, tagId);
                return Ok(new { exists });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBlogTagDTO dto)
        {
            try
            {
                await _blogTagService.CreateAsync(dto);
                return Ok("Yeni BlogTag kaydı oluşturuldu.");
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
        public async Task<IActionResult> Put([FromBody] UpdateBlogTagDTO dto)
        {
            try
            {
                await _blogTagService.UpdateAsync(dto);
                return Ok("BlogTag güncellendi.");
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
                var deleted = await _blogTagService.DeleteByIdAsync(id);
                if (!deleted)
                    return NotFound("BlogTag bulunamadı.");

                return Ok("BlogTag başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Silme hatası: {ex.Message}");
            }
        }
    }
}
