using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IGenericService<Contact> _contactService;
        private readonly IMapper _mapper;

        public ContactsController(IGenericService<Contact> contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contactList = await _contactService.GetListAsync();
            var dtos = _mapper.Map<List<ResultContactDTO>>(contactList);
            return Ok(dtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {

            var contacts = await _contactService.GetByIdAsync(id);
            if (contacts == null) return NotFound();
            var dtos = _mapper.Map<ResultContactDTO>(contacts);
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateContactDTO createContactDTO)
        {
            var dtos = _mapper.Map<Contact>(createContactDTO);
            await _contactService.CreateAsync(dtos);
            return Ok("Yeni İletişim Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateContactDTO updateContactDTO)
        {
            var dtos = _mapper.Map<Contact>(updateContactDTO);
            await _contactService.UpdateAsync(dtos);
            return Ok("İletişim Alanı Güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contacts = await _contactService.GetByIdAsync(id);
            if (contacts == null)
                return NotFound();

            await _contactService.DeleteAsync(contacts);
            return Ok();
        }
    }
}
