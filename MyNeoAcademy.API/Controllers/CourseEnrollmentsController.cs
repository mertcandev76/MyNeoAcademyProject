using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using System.Security.Claims;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  
    public class CourseEnrollmentsController : ControllerBase
    {
        private readonly ICourseEnrollmentService _courseEnrollmentService;

        public CourseEnrollmentsController(ICourseEnrollmentService courseEnrollmentService)
        {
            _courseEnrollmentService = courseEnrollmentService;
        }


        [HttpGet("byinstructor")]
        public async Task<IActionResult> GetByInstructorId()
        {
            var instructorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (instructorIdClaim == null || !int.TryParse(instructorIdClaim.Value, out int instructorId))
                return Unauthorized("InstructorId bilgisi bulunamadı veya geçersiz.");

            var filtered = await _courseEnrollmentService.GetEnrollmentsByInstructorIdAsync(instructorId);

            return Ok(filtered);
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _courseEnrollmentService.GetAllWithIncludesAsync();
            return Ok(enrollments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var enrollment = await _courseEnrollmentService.GetByIdWithIncludesAsync(id);
            if (enrollment == null)
                return NotFound("Enrollment not found.");

            return Ok(enrollment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseEnrollmentDTO dto)
        {
            await _courseEnrollmentService.CreateAsync(dto);
            return Ok("Enrollment created successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseEnrollmentService.DeleteByIdAsync(id);
            if (!deleted)
                return NotFound("Enrollment not found.");

            return Ok("Enrollment deleted successfully.");
        }
    }
}

