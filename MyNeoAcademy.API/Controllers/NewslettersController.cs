using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System.Data;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewslettersController : ControllerBase
    {
       
            private readonly IGenericService<Newsletter> _newsletterService;
            private readonly IMapper _mapper;

            public NewslettersController(IGenericService<Newsletter> newsletterService, IMapper mapper)
            {
                _newsletterService = newsletterService;
                _mapper = mapper;
            }

            // GET: api/Newsletters
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var list = await _newsletterService.GetListAsync();
                var dtoList = _mapper.Map<List<ResultNewsletterDTO>>(list);
                return Ok(dtoList);
            }

            // GET: api/Newsletters/5
            [HttpGet("{id:int}")]
            public async Task<IActionResult> Detail(int id)
            {
                var entity = await _newsletterService.GetByIdAsync(id);
                if (entity == null)
                    return NotFound("Newsletter not found.");

                var dto = _mapper.Map<ResultNewsletterDTO>(entity);
                return Ok(dto);
            }

            // POST: api/Newsletters
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateNewsletterDTO dto)
            {
                if (dto == null)
                    return BadRequest("Invalid data provided.");

                var entity = _mapper.Map<Newsletter>(dto);
            // Optionally set additional properties like the subscription date if not provided
            entity.SubscribedDate = DateTime.UtcNow;

            await _newsletterService.CreateAsync(entity);
                return CreatedAtAction(nameof(Detail), new { id = entity.NewsletterID }, "Newsletter subscription created successfully.");
            }

            // PUT: api/Newsletters
            [HttpPut]
            public async Task<IActionResult> Update([FromBody] UpdateNewsletterDTO dto)
            {
                var entity = await _newsletterService.GetByIdAsync(dto.NewsletterID);
                if (entity == null)
                    return NotFound("Newsletter not found.");

                _mapper.Map(dto, entity);
                await _newsletterService.UpdateAsync(entity);
                return Ok("Newsletter updated successfully.");
            }

            // DELETE: api/Newsletters/5
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(int id)
            {
                var entity = await _newsletterService.GetByIdAsync(id);
                if (entity == null)
                    return NotFound("Newsletter not found.");

                await _newsletterService.DeleteAsync(entity);
                return Ok("Newsletter subscription deleted successfully.");
            }
        
    }
}
