using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.API.Utilities;
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
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CommentsController(ICommentService commentService, IMapper mapper, IWebHostEnvironment env)
        {
            _commentService = commentService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _commentService.GetAllWithBlogAsync();
            var dtoList = _mapper.Map<List<ResultCommentDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _commentService.GetByIdWithBlogAsync(id);
            if (entity == null)
                return NotFound("Yorum bulunamadı.");

            var dto = _mapper.Map<ResultCommentDTO>(entity);
            return Ok(dto);
        }

        // ** Yeni endpoint: blogId ile filtreli yorumlar **
        [HttpGet("byblog/{blogId:int}")]
        public async Task<IActionResult> GetByBlog(int blogId)
        {
            var list = await _commentService.GetAllByBlogIdAsync(blogId);
            var dtoList = _mapper.Map<List<ResultCommentDTO>>(list);
            return Ok(dtoList);
        }

        // Kullanıcı yorumları (resim yok)
        [HttpPost]
        [Route("create-user-comment")]
        public async Task<IActionResult> CreateUserComment([FromBody] CreateCommentDTO dto)
        {
            var entity = _mapper.Map<Comment>(dto);
            entity.CreatedDate = DateTime.Now;

            await _commentService.CreateAsync(entity);

            return CreatedAtAction(nameof(Detail), new { id = entity.CommentID }, "Yorum başarıyla eklendi.");
        }
        
        // Admin yorumları (resim zorunlu)
        [HttpPost]
        [Route("create-admin-comment")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAdminComment([FromForm] CreateCommentWithFileDTO dto)
        {
            if (dto.ImageFile == null)
                return BadRequest("Yorum görseli zorunludur.");

            var entity = _mapper.Map<Comment>(dto);
            string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/comments");
            entity.ImageUrl = imagePath;
            entity.CreatedDate = DateTime.Now;

            await _commentService.CreateAsync(entity);

            return CreatedAtAction(nameof(Detail), new { id = entity.CommentID }, "Yorum başarıyla eklendi.");
        }


        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateCommentWithFileDTO dto)
        {
            var entity = await _commentService.GetByIdAsync(dto.CommentID);
            if (entity == null)
                return NotFound("Yorum bulunamadı.");

            _mapper.Map(dto, entity);

            if (dto.ImageFile != null)
            {
                string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/comments");
                entity.ImageUrl = imagePath;
            }

            await _commentService.UpdateAsync(entity);
            return Ok("Yorum güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _commentService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Yorum bulunamadı.");

            await _commentService.DeleteAsync(entity);
            return Ok("Yorum silindi.");
        }
    }
}
