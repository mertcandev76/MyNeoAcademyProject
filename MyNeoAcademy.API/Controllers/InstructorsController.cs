using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public InstructorsController(IGenericService<Instructor> instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        // Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var instructorList = await _instructorService.GetListAsync();
            var dtos = _mapper.Map<List<ResultInstructorDTO>>(instructorList);
            return Ok(dtos);
        }

        // ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null) return NotFound();
            var dto = _mapper.Map<ResultInstructorDTO>(instructor);
            return Ok(dto);
        }

        // Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorDTO createInstructorDTO)
        {
            var entity = _mapper.Map<Instructor>(createInstructorDTO);
            await _instructorService.CreateAsync(entity);
            return Ok("Yeni eğitmen eklendi");
        }

        // Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInstructorDTO updateInstructorDTO)
        {
            var entity = _mapper.Map<Instructor>(updateInstructorDTO);
            await _instructorService.UpdateAsync(entity);
            return Ok("Eğitmen bilgileri güncellendi");
        }

        // Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
                return NotFound();

            await _instructorService.DeleteAsync(instructor);
            return Ok("Eğitmen silindi");
        }
    }
}
