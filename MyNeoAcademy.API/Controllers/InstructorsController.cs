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
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IWebHostEnvironment _env;

        public InstructorsController(IInstructorService instructorService, IWebHostEnvironment env)
        {
            _instructorService = instructorService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var instructors = await _instructorService.GetAllWithIncludesAsync();
            return Ok(instructors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var instructor = await _instructorService.GetByIdWithIncludesAsync(id);
            if (instructor == null)
                return NotFound("Eğitmen bulunamadı.");

            return Ok(instructor);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CreateInstructorWithFileDTO dto)
        {
            await _instructorService.CreateWithFileAsync(dto, _env.WebRootPath);
            return Ok("Yeni eğitmen kaydı oluşturuldu.");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] UpdateInstructorWithFileDTO dto)
        {
            await _instructorService.UpdateWithFileAsync(dto, _env.WebRootPath);
            return Ok("Eğitmen güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _instructorService.DeleteByIdAsync(id);
            if (!deleted)
                return NotFound("Eğitmen bulunamadı.");

            return Ok("Eğitmen başarıyla silindi.");
        }
    }
}

