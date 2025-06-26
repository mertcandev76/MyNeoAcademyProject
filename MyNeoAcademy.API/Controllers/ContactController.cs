using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IGenericService<Contact> _contactService;
        private readonly IMapper _mapper;

        public ContactController(IGenericService<Contact> contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values= await _contactService.TGetListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var values= await _contactService.TGetByIdAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateContactDTO createContactDTO)
        {
            var dtos= _mapper.Map<Contact>(createContactDTO);
            await _contactService.TCreateAsync(dtos);
            return Ok("Yeni İletişim Alanı Oluşturuldu.");
        }
     
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateContactDTO updateContactDTO)
        {
            var dtos = _mapper.Map<Contact>(updateContactDTO);
            await _contactService.TUpdateAsync(dtos);
            return Ok("Yeni İletişim Alanı Güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.TDeleteAsync(id);
            return Ok("Yeni İletişim Alanı Silindi.");
        }
    }
}
