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
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _instructorService.GetByIdAsync(id);
            if (entity == null) return NotFound("Eğitmen bulunamadı.");
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
            return CreatedAtAction(nameof(Detail), new { id = entity.InstructorID }, "Yeni eğitmen eklendi.");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateInstructorWithFileDTO dto)
        {
            var entity = await _instructorService.GetByIdAsync(dto.InstructorID);
            if (entity == null)
                return NotFound("Eğitmen bulunamadı.");

            

            _mapper.Map(dto, entity);
            if (dto.ImageFile != null)
            {
                string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/instructors");
                entity.ImageUrl = imagePath;
            }

            await _instructorService.UpdateAsync(entity);
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
