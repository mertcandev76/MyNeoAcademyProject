using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IGenericService<About> _aboutService;
        private readonly IMapper _mapper;
        public AboutsController(IGenericService<About> aboutService, IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }

        //Listeleme(GET: api/About)
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var aboutList = await _aboutService.GetListAsync();
            return Ok(_mapper.Map<List<ResultAboutDTO>>(aboutList));
        }
        //ID ile Getirme(GET: api/About/5)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var abouts = await _aboutService.GetByIdAsync(id);
            if (abouts == null) return NotFound();
            return Ok(_mapper.Map<ResultAboutDTO>(abouts));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var abouts = await _aboutService.GetByIdAsync(id);
            if (abouts == null)
                return NotFound();

            await _aboutService.DeleteAsync(abouts);
            return Ok();
        }

        ////Silme( DELETE: api/About/5)
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _aboutService.DeleteAsync(id);
        //    return Ok("Hakkımızda Silindi");
        //}
        //Ekleme(POST: api/About)
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutDTO createAboutDTO)
        {
            var dtos= _mapper.Map<About>(createAboutDTO);
            await _aboutService.CreateAsync(dtos);
            return Ok("Yeni Hakkımzda Alanı Oluşturuldu");
        }
        //Güncelleme(PUT: api/About/5)
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAboutDTO updateAboutDTO)
        {
            var dtos= _mapper.Map<About>(updateAboutDTO);
            await _aboutService.UpdateAsync(dtos);
            return Ok("Hakkımızda Alanı Güncellendi");
        }

    }
}
