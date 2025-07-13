using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.CommentDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var commentList = await _commentService.GetAllWithBlogAsync();
            var dtos = _mapper.Map<List<ResultCommentDTO>>(commentList);
            return Ok(dtos);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentService.GetByIdWithBlogAsync(id);
            if (comment == null)
                return NotFound();

            var dto = _mapper.Map<ResultCommentDTO>(comment);
            return Ok(dto);
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDTO createCommentDTO)
        {
            var entity = _mapper.Map<Comment>(createCommentDTO);
            await _commentService.CreateAsync(entity);
            return Ok("Yorum başarıyla eklendi.");
        }

        // PUT: api/Comments
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommentDTO updateCommentDTO)
        {
            var entity = _mapper.Map<Comment>(updateCommentDTO);
            await _commentService.UpdateAsync(entity);
            return Ok("Yorum başarıyla güncellendi.");
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _commentService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            await _commentService.DeleteAsync(entity);
            return Ok("Yorum başarıyla silindi.");
        }
    }
}
