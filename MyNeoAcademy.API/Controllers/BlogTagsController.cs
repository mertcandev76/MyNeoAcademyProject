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
        private readonly IMapper _mapper;

        public BlogTagsController(IBlogTagService blogTagService, IMapper mapper)
        {
            _blogTagService = blogTagService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _blogTagService.GetAllWithIncludesAsync();
            var dtoList = _mapper.Map<List<ResultBlogTagDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _blogTagService.GetByIdWithIncludesAsync(id);
            if (entity == null)
                return NotFound("Blog-Tag ilişkisi bulunamadı.");

            var dto = _mapper.Map<ResultBlogTagDTO>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBlogTagDTO dto)
        {
            // Çift kayıt kontrolü için servis metodu kullanılabilir
            var exists = await _blogTagService.ExistsAsync(dto.BlogID, dto.TagID);
            if (exists)
                return BadRequest("Bu blog ve etiket ilişkisi zaten mevcut.");

            var entity = _mapper.Map<BlogTag>(dto);
            await _blogTagService.CreateAsync(entity);
            return CreatedAtAction(nameof(Detail), new { id = entity.BlogTagID }, "Blog-Tag ilişkisi oluşturuldu.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBlogTagDTO dto)
        {
            var entity = await _blogTagService.GetByIdAsync(dto.BlogTagID);
            if (entity == null)
                return NotFound("Blog-Tag ilişkisi bulunamadı.");

            _mapper.Map(dto, entity);
            await _blogTagService.UpdateAsync(entity);
            return Ok("Blog-Tag ilişkisi güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _blogTagService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Blog-Tag ilişkisi bulunamadı.");

            await _blogTagService.DeleteAsync(entity);
            return Ok("Blog-Tag ilişkisi silindi.");
        }
    }
}
