using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.AboutDetailDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutDetailsController : ControllerBase
    {
        private readonly IGenericService<AboutDetail> _aboutDetailService;
        private readonly IMapper _mapper;

        public AboutDetailsController(IGenericService<AboutDetail> aboutDetailService, IMapper mapper)
        {
            _aboutDetailService = aboutDetailService;
            _mapper = mapper;
        }

        // GET: api/AboutDetails
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var aboutDetailList = await _aboutDetailService.GetListAsync();
            var dtos = _mapper.Map<List<ResultAboutDetailDTO>>(aboutDetailList);
            return Ok(dtos);
        }

        // GET: api/AboutDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aboutDetail = await _aboutDetailService.GetByIdAsync(id);
            if (aboutDetail == null) return NotFound();

            var dto = _mapper.Map<ResultAboutDetailDTO>(aboutDetail);
            return Ok(dto);
        }

        // POST: api/AboutDetails
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutDetailDTO createAboutDetailDTO)
        {
            var entity = _mapper.Map<AboutDetail>(createAboutDetailDTO);
            await _aboutDetailService.CreateAsync(entity);
            return Ok("Yeni about detail başarıyla eklendi.");
        }

        // PUT: api/AboutDetails
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAboutDetailDTO updateAboutDetailDTO)
        {
            var entity = _mapper.Map<AboutDetail>(updateAboutDetailDTO);
            await _aboutDetailService.UpdateAsync(entity);
            return Ok("About detail başarıyla güncellendi.");
        }

        // DELETE: api/AboutDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _aboutDetailService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            await _aboutDetailService.DeleteAsync(entity);
            return Ok("About detail başarıyla silindi.");
        }
    }
}
