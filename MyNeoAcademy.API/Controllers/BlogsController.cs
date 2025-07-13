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
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;   

        public BlogsController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
         public async Task<IActionResult> Get()
        {
            var blogList = await _blogService.GetAllWithCategoryAndAuthorAsync(); // Entity List<Blog>
            var dtos = _mapper.Map<List<ResultBlogDTO>>(blogList); // DTO List<ResultBlogDTO>
            return Ok(dtos); // DTO döndür

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {

            var blogs = await _blogService.GetByIdWithCategoryAndAuthorAsync(id);
            if (blogs == null)
                return NotFound();

            var dto = _mapper.Map<ResultBlogDTO>(blogs);
            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogDTO createBlogDTO)
        {
            var dtos= _mapper.Map<Blog>(createBlogDTO);
            await _blogService.CreateAsync(dtos);
            return Ok("Yeni Blog Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBlogDTO updateBlogDTO)
        {

            var dtos = _mapper.Map<Blog>(updateBlogDTO);
            await _blogService.UpdateAsync(dtos);

            return Ok("Blog başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blogs = await _blogService.GetByIdAsync(id);
            if (blogs == null)
                return NotFound();

            await _blogService.DeleteAsync(blogs);
            return Ok();
        }

    }
}
