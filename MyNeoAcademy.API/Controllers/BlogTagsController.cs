using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BlogTagDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTagsController : ControllerBase
    {
        private readonly IBlogTagService _blogTagService;
        private readonly IMapper _mapper;

        public BlogTagsController(IBlogTagService blogTagService, IMapper mapper)
        {
            _blogTagService = blogTagService;
            _mapper = mapper;
        }

        // GET: api/BlogTags
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var blogTagList = await _blogTagService.GetAllWithBlogAndTagAsync();
            var dtos = _mapper.Map<List<ResultBlogTagDTO>>(blogTagList);
            return Ok(dtos);
        }

        // GET: api/BlogTags/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var blogTag = await _blogTagService.GetByIdWithBlogAndTagAsync(id);
            if (blogTag == null)
                return NotFound();

            var dto = _mapper.Map<ResultBlogTagDTO>(blogTag);
            return Ok(dto);
        }

        // POST: api/BlogTags
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogTagDTO createBlogTagDTO)
        {
            var entity = _mapper.Map<BlogTag>(createBlogTagDTO);
            await _blogTagService.CreateAsync(entity);
            return Ok("Yeni blog-tag ilişkisi başarıyla eklendi.");
        }

        // PUT: api/BlogTags
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBlogTagDTO updateBlogTagDTO)
        {
            var entity = _mapper.Map<BlogTag>(updateBlogTagDTO);
            await _blogTagService.UpdateAsync(entity);
            return Ok("Blog-tag ilişkisi başarıyla güncellendi.");
        }

        // DELETE: api/BlogTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _blogTagService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            await _blogTagService.DeleteAsync(entity);
            return Ok("Blog-tag ilişkisi başarıyla silindi.");
        }
    }
}
