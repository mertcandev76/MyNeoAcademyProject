using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
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
            var bannerList = await _bannerService.GetListAsync();
            var dtos = _mapper.Map<List<ResultBannerDTO>>(bannerList);
            return Ok(dtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {

            var banners = await _bannerService.GetByIdAsync(id);
            if (banners == null) return NotFound();
            var dtos = _mapper.Map<ResultBannerDTO>(banners);
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBannerDTO createBannerDTO)
        {
            var dtos= _mapper.Map<Banner>(createBannerDTO);
            await _bannerService.CreateAsync(dtos);
            return Ok("Yeni Afiş Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateBannerDTO updateBannerDTO)
        {
            var dtos = _mapper.Map<Banner>(updateBannerDTO);
            await _bannerService.UpdateAsync(dtos);
            return Ok("Afiş Alanı Güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var banners = await _bannerService.GetByIdAsync(id);
            if (banners == null)
                return NotFound();

            await _bannerService.DeleteAsync(banners);
            return Ok();
        }

    }
}
