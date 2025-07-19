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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public CoursesController(ICourseService courseService, IMapper mapper, IWebHostEnvironment env)
        {
            _courseService = courseService;
            _mapper = mapper;
            _env = env;
        }

    
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var list = await _courseService.GetAllWithCategoryAndInstructorAsync(); // Entity List<Blog>
            var dtoList = _mapper.Map<List<ResultCourseDTO>>(list); // DTO List<ResultBlogDTO>
            return Ok(dtoList); // DTO döndür
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _courseService.GetByIdWithCategoryAndInstructorAsync(id);
            if (entity == null) return NotFound("Kurs bulunamadı.");
            var dto = _mapper.Map<ResultCourseDTO>(entity);
            return Ok(dto);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateCourseWithFileDTO dto)
        {
            if (dto.ImageFile == null)
                return BadRequest("Bir profil fotoğrafı seçilmelidir.");
            string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/courses");
            var entity = _mapper.Map<Course>(dto);
            entity.ImageUrl = imagePath;
            await _courseService.CreateAsync(entity);
            return CreatedAtAction(nameof(Detail), new { id = entity.CourseID }, "Yeni kurs eklendi.");
        }
        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateCourseWithFileDTO dto)
        {
            var entity = await _courseService.GetByIdAsync(dto.CourseID);
            if (entity == null)
                return NotFound("Kurs bulunamadı.");
            
           
            _mapper.Map(dto, entity);
            if (dto.ImageFile != null)
            {
                // Dosyayı kaydet (FileHelper kendi helperın, dosya yolu ve klasör adını ihtiyacına göre ayarla)
                string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/courses");
                entity.ImageUrl = imagePath;
            }

            await _courseService.UpdateAsync(entity);
            return Ok("Kurs Güncellendi");
        }
        //Silme
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _courseService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Kurs bulunamadı.");

            await _courseService.DeleteAsync(entity);
            return Ok("Kurs silindi.");
        }
    }
}
