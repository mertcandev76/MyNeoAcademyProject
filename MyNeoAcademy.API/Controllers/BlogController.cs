using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IGenericService<Blog> _blogService;
        private readonly IMapper _mapper;

        public BlogController(IGenericService<Blog> blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
         public async Task<IActionResult> Get()
        {
            var values= await _blogService.TGetListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var values = await _blogService.TGetByIdAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogDTO createBlogDTO)
        {
            var dtos= _mapper.Map<Blog>(createBlogDTO);
            await _blogService.TCreateAsync(dtos);
            return Ok("Yeni Blog Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateBlogDTO updateBlogDTO)
        {
            var dtos = _mapper.Map<Blog>(updateBlogDTO);
            await _blogService.TUpdateAsync(dtos);
            return Ok("Blog Alanı Güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.TDeleteAsync(id);
            return Ok("Blog Alanı Silindi.");
        }
     
    }
}
