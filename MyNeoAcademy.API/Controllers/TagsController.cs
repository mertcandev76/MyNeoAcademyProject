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
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagsController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        // GET: api/tags
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _tagService.GetAllWithIncludesAsync();
            var dtoList = _mapper.Map<List<ResultTagDTO>>(list);
            return Ok(dtoList);
        }

        // GET: api/tags/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _tagService.GetByIdWithIncludesAsync(id);
            if (entity == null)
                return NotFound("Etiket bulunamadı.");

            var dto = _mapper.Map<ResultTagDTO>(entity);
            return Ok(dto);
        }

        // POST: api/tags
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTagDTO dto)
        {
            var entity = _mapper.Map<Tag>(dto);
            await _tagService.CreateAsync(entity);

            return CreatedAtAction(nameof(Detail), new { id = entity.TagID }, "Yeni etiket eklendi.");
        }

        // PUT: api/tags
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTagDTO dto)
        {
            var entity = await _tagService.GetByIdAsync(dto.TagID);
            if (entity == null)
                return NotFound("Etiket bulunamadı.");

            _mapper.Map(dto, entity);

            await _tagService.UpdateAsync(entity);
            return Ok("Etiket güncellendi.");
        }

        // DELETE: api/tags/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _tagService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Etiket bulunamadı.");

            await _tagService.DeleteAsync(entity);
            return Ok("Etiket silindi.");
        }
    }
}
