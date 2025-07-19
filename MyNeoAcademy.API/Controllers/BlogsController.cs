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

    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BlogsController(IBlogService blogService, IMapper mapper, IWebHostEnvironment env)
        {
            _blogService = blogService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _blogService.GetAllWithIncludesAsync();
            var dtoList = _mapper.Map<List<ResultBlogDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _blogService.GetByIdWithIncludesAsync(id);
            if (entity == null) return NotFound("Blog bulunamadı.");
            var dto = _mapper.Map<ResultBlogDTO>(entity);
            return Ok(dto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateBlogWithFileDTO dto)
        {
            if (dto.ImageFile == null)
                return BadRequest("Bir görsel dosyası seçilmelidir.");

            string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/blogs");
            var entity = _mapper.Map<Blog>(dto);
            entity.ImageUrl = imagePath;

            await _blogService.CreateAsync(entity);
            return CreatedAtAction(nameof(Detail), new { id = entity.BlogID }, "Yeni blog eklendi.");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateBlogWithFileDTO dto)
        {
            var entity = await _blogService.GetByIdAsync(dto.BlogID);
            if (entity == null)
                return NotFound("Blog bulunamadı.");

            _mapper.Map(dto, entity);

            if (dto.ImageFile != null)
            {
                string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/blogs");
                entity.ImageUrl = imagePath;
            }

            await _blogService.UpdateAsync(entity);
            return Ok("Blog güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _blogService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Blog bulunamadı.");

            await _blogService.DeleteAsync(entity);
            return Ok("Blog silindi.");
        }
    }
}
