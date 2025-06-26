using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IGenericService<BlogCategory> _blogCategoryService;
        private readonly IMapper _mapper;

        public BlogCategoryController(IGenericService<BlogCategory> blogCategoryService, IMapper mapper)
        {
            _blogCategoryService = blogCategoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> Get()
        {
            var values= await _blogCategoryService.TGetListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var values = await _blogCategoryService.TGetByIdAsync(id);
            if (values == null) NotFound();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogCategoryDTO createBlogCategoryDTO)
        {
            var dtos= _mapper.Map<BlogCategory>(createBlogCategoryDTO);
            await _blogCategoryService.TCreateAsync(dtos);
            return Ok("Yeni Kategori Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateBlogCategoryDTO updateBlogCategoryDTO)
        {
            var dtos = _mapper.Map<BlogCategory>(updateBlogCategoryDTO);
            await _blogCategoryService.TUpdateAsync(dtos);
            return Ok("Kategori Alanı Güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogCategoryService.TDeleteAsync(id);
            return Ok("Kategori Alanı Silindi.");
        }
        
    }
}
