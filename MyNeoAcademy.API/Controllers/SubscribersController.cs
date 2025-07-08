using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.SocialMediaDTOs;
using MyNeoAcademy.DTO.DTOs.SubscriberDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly IGenericService<Subscriber> _subscriberService;
        private readonly IMapper _mapper;

        public SubscribersController(IGenericService<Subscriber> subscriberService, IMapper mapper)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
        }
        //Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var subscriberList = await _subscriberService.GetListAsync();
            var dtos = _mapper.Map<List<ResultSubscriberDTO>>(subscriberList);
            return Ok(dtos);
        }
        //ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subscribers = await _subscriberService.GetByIdAsync(id);
            if (subscribers == null) return NotFound();
            var dtos = _mapper.Map<ResultSubscriberDTO>(subscribers);
            return Ok(dtos);
        }
        //Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberDTO  createSubscriberDTO)
        {
            var dtos = _mapper.Map<Subscriber>(createSubscriberDTO);
            await _subscriberService.CreateAsync(dtos);
            return Ok("Yeni Abonelik Alanı Oluşturuldu");
        }
        //Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateSubscriberDTO  updateSubscriberDTO)
        {
            var dtos = _mapper.Map<Subscriber>(updateSubscriberDTO);
            await _subscriberService.UpdateAsync(dtos);
            return Ok("Abonelik Alanı Güncellendi");
        }
        //Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subscribers = await _subscriberService.GetByIdAsync(id);
            if (subscribers == null)
                return NotFound();

            await _subscriberService.DeleteAsync(subscribers);
            return Ok();
        }
    }
}
