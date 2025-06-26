using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BannerDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IGenericService<Banner> _bannerService;
        private readonly IMapper _mapper;

        public BannersController(IGenericService<Banner> bannerService, IMapper mapper)
        {
            _bannerService = bannerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values= await _bannerService.TGetListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var values = await _bannerService.TGetByIdAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBannerDTO dto)
        {
            var entities= _mapper.Map<Banner>(dto);
            await _bannerService.TCreateAsync(entities);
            return Ok("Yeni Afiş Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Create(UpdateBannerDTO dto)
        {
            var entities = _mapper.Map<Banner>(dto);
            await _bannerService.TUpdateAsync(entities);
            return Ok("Afiş Alanı Güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bannerService.TDeleteAsync(id);
            return Ok("Hakkımızda Silindi");
        }
    

    }
}
