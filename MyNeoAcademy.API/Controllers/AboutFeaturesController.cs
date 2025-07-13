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
        private readonly IAboutFeatureService _aboutFeatureService;
        private readonly IMapper _mapper;

        public AboutFeaturesController(IAboutFeatureService aboutFeatureService, IMapper mapper)
        {
            _aboutFeatureService = aboutFeatureService;
            _mapper = mapper;
        }

        // GET: api/AboutFeatures
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var aboutFeatureList = await _aboutFeatureService.GetAllWithAboutAsync(); // Entity List<AboutFeature>
            var dtos = _mapper.Map<List<ResultAboutFeatureDTO>>(aboutFeatureList);     // DTO List<ResultAboutFeatureDTO>
            return Ok(dtos);
        }

        // GET: api/AboutFeatures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var aboutFeature = await _aboutFeatureService.GetByIdWithBlogAboutAsync(id);
            if (aboutFeature == null)
                return NotFound();

            var dto = _mapper.Map<ResultAboutFeatureDTO>(aboutFeature);
            return Ok(dto);
        }
        // POST: api/AboutFeatures
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutFeatureDTO createAboutFeatureDTO)
        {
            var entity = _mapper.Map<AboutFeature>(createAboutFeatureDTO);
            await _aboutFeatureService.CreateAsync(entity);
            return Ok("Yeni özellik başarıyla eklendi.");
        }

        // PUT: api/AboutFeatures
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAboutFeatureDTO updateAboutFeatureDTO)
        {
            var entity = _mapper.Map<AboutFeature>(updateAboutFeatureDTO);
            await _aboutFeatureService.UpdateAsync(entity);
            return Ok("Özellik başarıyla güncellendi.");
        }

        // DELETE: api/AboutFeatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _aboutFeatureService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            await _aboutFeatureService.DeleteAsync(entity);
            return Ok("Özellik başarıyla silindi.");
        }
    }
}
