using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.API.Utilities;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AuthorsController(IAuthorService authorService, IMapper mapper, IWebHostEnvironment env)
        {
            _authorService = authorService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _authorService.GetListAsync();
            var dtoList = _mapper.Map<List<ResultAuthorDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _authorService.GetAllWithBlogAsync(id);
            if (entity == null)
                return NotFound("Yazar bulunamadı.");

            var dto = _mapper.Map<ResultAuthorDTO>(entity);
            return Ok(dto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateAuthorWithFileDTO dto)
        {
            if (dto.ImageFile == null)
                return BadRequest("Profil resmi zorunludur.");

            string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/authors");
            var entity = _mapper.Map<Author>(dto);
            entity.ImageUrl = imagePath;

            await _authorService.CreateAsync(entity);
            return CreatedAtAction(nameof(Detail), new { id = entity.AuthorID }, "Yeni yazar eklendi.");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateAuthorWithFileDTO dto)
        {
            var entity = await _authorService.GetByIdAsync(dto.AuthorID);
            if (entity == null)
                return NotFound("Yazar bulunamadı.");


            _mapper.Map(dto, entity);

            if (dto.ImageFile != null)
            {
                string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/authors");
                entity.ImageUrl = imagePath;
            }

            await _authorService.UpdateAsync(entity);
            return Ok("Yazar güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _authorService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Yazar bulunamadı.");

            await _authorService.DeleteAsync(entity);
            return Ok("Yazar silindi.");
        }
    }
}
