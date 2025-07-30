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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _env;

        public CommentsController(ICommentService commentService, IWebHostEnvironment env)
        {
            _commentService = commentService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await _commentService.GetAllWithIncludesAsync();
                return Ok(comments);
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
                var comment = await _commentService.GetByIdWithIncludesAsync(id);
                if (comment == null)
                    return NotFound("Yorum bulunamadı.");

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpGet("ByBlog/{blogId:int}")]
        public async Task<IActionResult> GetByBlog(int blogId)
        {
            try
            {
                var comments = await _commentService.GetByIdWithIncludesBlogAsync(blogId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        // Yeni: blogId'ye göre sayfalı getirme
        [HttpGet("pagedbyblog")]
        public async Task<IActionResult> GetPagedByBlog([FromQuery] int blogId, [FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            try
            {
                var pagedResult = await _commentService.GetPagedByBlogAsync(blogId, page, pageSize);
                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sayfalama hatası: {ex.Message}");
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            try
            {
                var pagedResult = await _commentService.GetPagedAsync(page, pageSize);
                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sayfalama hatası: {ex.Message}");
            }
        }

        [HttpPost("create-user-comment")]
        public async Task<IActionResult> CreateUserComment([FromBody] CreateCommentDTO dto)
        {
            try
            {
                await _commentService.CreateUserCommentAsync(dto);
                return Ok("Yorum başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Yorum ekleme hatası: {ex.Message}");
            }
        }

        [HttpPost("create-admin-comment")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAdminComment([FromForm] CreateCommentWithFileDTO dto)
        {
            try
            {
                await _commentService.CreateWithFileAsync(dto, _env.WebRootPath);
                return Ok("Yönetici yorumu başarıyla eklendi.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Yorum ekleme hatası: {ex.Message}");
            }
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] UpdateCommentWithFileDTO dto)
        {
            try
            {
                await _commentService.UpdateWithFileAsync(dto, _env.WebRootPath);
                return Ok("Yorum güncellendi.");
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
                var deleted = await _commentService.DeleteByIdAsync(id);
                if (!deleted)
                    return NotFound("Yorum bulunamadı.");

                return Ok("Yorum başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Silme hatası: {ex.Message}");
            }
        }
    }
}
