using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.TagDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IGenericService<Tag> _tagService;
        private readonly IMapper _mapper;

        public TagsController(IGenericService<Tag> tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        // Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tagList = await _tagService.GetListAsync();
            var dtos = _mapper.Map<List<ResultTagDTO>>(tagList);
            return Ok(dtos);
        }

        // ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null) return NotFound();
            var dto = _mapper.Map<ResultTagDTO>(tag);
            return Ok(dto);
        }

        // Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDTO createTagDTO)
        {
            var entity = _mapper.Map<Tag>(createTagDTO);
            await _tagService.CreateAsync(entity);
            return Ok("Yeni tag eklendi");
        }

        // Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateTagDTO updateTagDTO)
        {
            var entity = _mapper.Map<Tag>(updateTagDTO);
            await _tagService.UpdateAsync(entity);
            return Ok("Tag güncellendi");
        }

        // Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null)
                return NotFound();

            await _tagService.DeleteAsync(tag);
            return Ok("Tag silindi");
        }
    }
}
