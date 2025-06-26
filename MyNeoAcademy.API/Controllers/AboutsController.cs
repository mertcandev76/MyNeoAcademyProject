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

        //Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _aboutService.TGetListAsync();
            return Ok(values);
        }
        //ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var values = await _aboutService.TGetByIdAsync(id);
            return Ok(values);
        }

        //Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _aboutService.TDeleteAsync(id);
            return Ok("Hakkımızda Silindi");
        }
        //Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutDTO createAboutDTO)
        {
            var newValues= _mapper.Map<About>(createAboutDTO);
            await _aboutService.TCreateAsync(newValues);
            return Ok("Yeni Hakkımzda Alanı Oluşturuldu");
        }
        //Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAboutDTO updateAboutDTO)
        {
            var newValues= _mapper.Map<About>(updateAboutDTO);
            await _aboutService.TUpdateAsync(newValues);
            return Ok("Hakkımızda Alanı Güncellendi");
        }

    }
}
