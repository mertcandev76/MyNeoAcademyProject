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
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AboutsController(IAboutService aboutService, IMapper mapper, IWebHostEnvironment env)
        {
            _aboutService = aboutService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _aboutService.GetListAsync();
            var dtoList = _mapper.Map<List<ResultAboutDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _aboutService.GetAllWithAboutFeatureAsync(id);
            if (entity == null)
                return NotFound("About kaydı bulunamadı.");

            var dto = _mapper.Map<ResultAboutDTO>(entity);
            return Ok(dto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateAboutWithFileDTO dto)
        {
            if (dto.ImageFrontFile == null || dto.ImageBackFile == null)
                return BadRequest("Ön ve arka görseller zorunludur.");

            var entity = _mapper.Map<About>(dto);

            entity.ImageFrontUrl = await FileHelper.SaveFileAsync(dto.ImageFrontFile, _env.WebRootPath, "img/about");
            entity.ImageBackUrl = await FileHelper.SaveFileAsync(dto.ImageBackFile, _env.WebRootPath, "img/about");

            await _aboutService.CreateAsync(entity);
            return CreatedAtAction(nameof(Detail), new { id = entity.AboutID }, "Yeni hakkında bilgisi oluşturuldu.");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateAboutWithFileDTO dto)
        {
            var entity = await _aboutService.GetByIdAsync(dto.AboutID);
            if (entity == null)
                return NotFound("About kaydı bulunamadı.");


            _mapper.Map(dto, entity);

            if (dto.ImageFrontFile != null)
            {
                entity.ImageFrontUrl = await FileHelper.SaveFileAsync(dto.ImageFrontFile, _env.WebRootPath, "img/about");
            }

            if (dto.ImageBackFile != null)
            {
                entity.ImageBackUrl = await FileHelper.SaveFileAsync(dto.ImageBackFile, _env.WebRootPath, "img/about");
            }

            await _aboutService.UpdateAsync(entity);
            return Ok("About kaydı güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _aboutService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("About kaydı bulunamadı.");

            await _aboutService.DeleteAsync(entity);
            return Ok("About kaydı silindi.");
        }
    }
}
