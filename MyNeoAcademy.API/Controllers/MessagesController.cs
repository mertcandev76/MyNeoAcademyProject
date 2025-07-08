using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.MessageDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IGenericService<Message> _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IGenericService<Message> messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        //Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var messageList = await _messageService.GetListAsync();
            var dtos = _mapper.Map<List<ResultMessageDTO>>(messageList); 
            return Ok(dtos);
        }
        //ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var messages = await _messageService.GetByIdAsync(id);
            if (messages == null) return NotFound();
            var dtos = _mapper.Map<ResultMessageDTO>(messages);
            return Ok(dtos);
        }
        //Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageDTO createMessageDTO)
        {
            var dtos = _mapper.Map<Message>(createMessageDTO);
            await _messageService.CreateAsync(dtos);
            return Ok("Yeni Mesaj Alanı Oluşturuldu");
        }
        //Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateMessageDTO updateMessageDTO)
        {
            var dtos = _mapper.Map<Message>(updateMessageDTO);
            await _messageService.UpdateAsync(dtos);
            return Ok("Mesaj Alanı Güncellendi");
        }
        //Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var messages = await _messageService.GetByIdAsync(id);
            if (messages == null)
                return NotFound();

            await _messageService.DeleteAsync(messages);
            return Ok();
        }

    }
}
