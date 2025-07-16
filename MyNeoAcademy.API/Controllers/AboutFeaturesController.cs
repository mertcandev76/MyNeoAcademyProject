using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.AboutFeatureDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutFeaturesController : ControllerBase
    {
      
            private readonly IGenericService<AboutFeature> _aboutFeatureService;
            private readonly IMapper _mapper;

            public AboutFeaturesController(IGenericService<AboutFeature> aboutFeatureService, IMapper mapper)
            {
                _aboutFeatureService = aboutFeatureService;
                _mapper = mapper;
            }

            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var list = await _aboutFeatureService.GetListAsync(); // About ile birlikte getir
                var dtoList = _mapper.Map<List<ResultAboutFeatureDTO>>(list);
                return Ok(dtoList);
            }

            [HttpGet("{id:int}")]
            public async Task<IActionResult> Detail(int id)
            {
                var entity = await _aboutFeatureService.GetByIdAsync(id);
                if (entity == null) return NotFound("Özellik hakkı bulunamadı.");
                var dto = _mapper.Map<ResultAboutFeatureDTO>(entity);
                return Ok(dto);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateAboutFeatureDTO dto)
            {
                var entity = _mapper.Map<AboutFeature>(dto);
                await _aboutFeatureService.CreateAsync(entity);
                return CreatedAtAction(nameof(Detail), new { id = entity.AboutFeatureID }, "Yeni özellik hakkı eklendi.");
            }

            [HttpPut]
            public async Task<IActionResult> Update([FromBody] UpdateAboutFeatureDTO dto)
            {
                var existing = await _aboutFeatureService.GetByIdAsync(dto.AboutFeatureID);
                if (existing == null) return NotFound("Özellik hakkı  bulunamadı.");

                // Güncelle
                existing.IconClass = dto.IconClass;
                existing.Text = dto.Text;
                existing.AboutID = dto.AboutID;

                await _aboutFeatureService.UpdateAsync(existing);
                return Ok("Özellik hakkı güncellendi.");
            }

            [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(int id)
            {
                var entity = await _aboutFeatureService.GetByIdAsync(id);
                if (entity == null) return NotFound("Özellik hakkı bulunamadı.");

                await _aboutFeatureService.DeleteAsync(entity);
                return Ok("Özellik hakkı silindi.");
            }
        
    }
}
