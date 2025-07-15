using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.API.Utilities;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IGenericService<Instructor> _instructorService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public InstructorsController(IGenericService<Instructor> instructorService, IMapper mapper, IWebHostEnvironment env)
        {
            _instructorService = instructorService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _instructorService.GetListAsync();
            var dtoList = _mapper.Map<List<ResultInstructorDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _instructorService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Eğitmen bulunamadı.");

            var dto = _mapper.Map<ResultInstructorDTO>(entity);
            return Ok(dto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateInstructorWithFileDTO dto)
        {
            if (dto.ImageFile == null)
                return BadRequest("Bir profil fotoğrafı seçilmelidir.");

            string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/instructors");
            var entity = _mapper.Map<Instructor>(dto);
            entity.ImageUrl = imagePath;

            await _instructorService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.InstructorID }, "Yeni eğitmen eklendi.");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateInstructorWithFileDTO dto)
        {
            var existing = await _instructorService.GetByIdAsync(dto.InstructorID);
            if (existing == null)
                return NotFound("Eğitmen bulunamadı.");

            // Alan güncellemeleri
            existing.FullName = dto.FullName;
            existing.Title = dto.Title;
            existing.Bio = dto.Bio;
            existing.FacebookUrl = dto.FacebookUrl;
            existing.TwitterUrl = dto.TwitterUrl;
            existing.WebsiteUrl = dto.WebsiteUrl;

            if (dto.ImageFile != null)
            {
                string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/instructors");
                existing.ImageUrl = imagePath;
            }

            await _instructorService.UpdateAsync(existing);
            return Ok("Eğitmen güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _instructorService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Eğitmen bulunamadı.");

            await _instructorService.DeleteAsync(entity);
            return Ok("Eğitmen silindi.");
        }
    }
}
