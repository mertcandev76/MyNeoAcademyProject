using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.MessageDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IGenericService<Message> _messageService;
        private readonly IMapper _mapper;

        public MessageController(IGenericService<Message> messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        //Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _messageService.TGetListAsync();
            return Ok(values);
        }
        //ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var values = await _messageService.TGetByIdAsync(id);
            return Ok(values);
        }
        //Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageDTO createMessageDTO)
        {
            var dtos = _mapper.Map<Message>(createMessageDTO);
            await _messageService.TCreateAsync(dtos);
            return Ok("Yeni Mesaj Alanı Oluşturuldu");
        }
        //Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateMessageDTO updateMessageDTO)
        {
            var dtos = _mapper.Map<Message>(updateMessageDTO);
            await _messageService.TUpdateAsync(dtos);
            return Ok("Mesaj Alanı Güncellendi");
        }
        //Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.TDeleteAsync(id);
            return Ok("Mesaj Silindi");
        }
  
    }
}
