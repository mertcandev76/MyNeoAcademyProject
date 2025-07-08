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

        // Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var socialMediaList = await _socialMediaService.GetListAsync();
            var dtos = _mapper.Map<List<ResultSocialMediaDTO>>(socialMediaList);
            return Ok(dtos);
        }

        // ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var socialMedia = await _socialMediaService.GetByIdAsync(id);
            if (socialMedia == null) return NotFound();
            var dto = _mapper.Map<ResultSocialMediaDTO>(socialMedia);
            return Ok(dto);
        }

        // Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateSocialMediaDTO createSocialMediaDTO)
        {
            var entity = _mapper.Map<SocialMedia>(createSocialMediaDTO);
            await _socialMediaService.CreateAsync(entity);
            return Ok("Yeni Sosyal Medya Kaydı Oluşturuldu");
        }

        // Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateSocialMediaDTO updateSocialMediaDTO)
        {
            var entity = _mapper.Map<SocialMedia>(updateSocialMediaDTO);
            await _socialMediaService.UpdateAsync(entity);
            return Ok("Sosyal Medya Kaydı Güncellendi");
        }

        // Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var socialMedia = await _socialMediaService.GetByIdAsync(id);
            if (socialMedia == null)
                return NotFound();

            await _socialMediaService.DeleteAsync(socialMedia);
            return Ok();
        }
    }
}
