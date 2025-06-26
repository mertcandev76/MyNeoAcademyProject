using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.MessageDTOs;
using MyNeoAcademy.DTO.DTOs.SocialMediaDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController : ControllerBase
    {
        private readonly IGenericService<SocialMedia> _socialMediaService;
        private readonly IMapper _mapper;

        public SocialMediasController(IGenericService<SocialMedia> socialMediaService, IMapper mapper)
        {
            _socialMediaService = socialMediaService;
            _mapper = mapper;
        }
        //Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _socialMediaService.TGetListAsync();
            return Ok(values);
        }
        //ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var values = await _socialMediaService.TGetByIdAsync(id);
            return Ok(values);
        }
        //Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateSocialMediaDTO createSocialMediaDTO)
        {
            var dtos = _mapper.Map<SocialMedia>(createSocialMediaDTO);
            await _socialMediaService.TCreateAsync(dtos);
            return Ok("Yeni Sosyal Media Alanı Oluşturuldu");
        }
        //Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateSocialMediaDTO  updateSocialMediaDTO)
        {
            var dtos = _mapper.Map<SocialMedia>(updateSocialMediaDTO);
            await _socialMediaService.TUpdateAsync(dtos);
            return Ok("Sosyal Media Alanı Güncellendi");
        }
        //Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _socialMediaService.TDeleteAsync(id);
            return Ok("Sosyal Media Silindi");
        }

    }
}
