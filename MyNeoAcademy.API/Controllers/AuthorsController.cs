using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.AuthorDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly IGenericService<Author> _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IGenericService<Author> authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        // Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var authorList = await _authorService.GetListAsync();
            var dtos = _mapper.Map<List<ResultAuthorDTO>>(authorList);
            return Ok(dtos);
        }

        // ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();
            var dto = _mapper.Map<ResultAuthorDTO>(author);
            return Ok(dto);
        }

        // Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDTO createAuthorDTO)
        {
            var entity = _mapper.Map<Author>(createAuthorDTO);
            await _authorService.CreateAsync(entity);
            return Ok("Yeni yazar eklendi");
        }

        // Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAuthorDTO updateAuthorDTO)
        {
            var entity = _mapper.Map<Author>(updateAuthorDTO);
            await _authorService.UpdateAsync(entity);
            return Ok("Yazar güncellendi");
        }

        // Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            await _authorService.DeleteAsync(author);
            return Ok("Yazar silindi");
        }
    }
}
