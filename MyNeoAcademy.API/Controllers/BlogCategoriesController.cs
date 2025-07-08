using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BannerDTOs;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoriesController : ControllerBase
    {
        private readonly IGenericService<BlogCategory> _blogCategoryService;
        private readonly IMapper _mapper;

        public BlogCategoriesController(IGenericService<BlogCategory> blogCategoryService, IMapper mapper)
        {
            _blogCategoryService = blogCategoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> Get()
        {
            var blogCategoryList = await _blogCategoryService.GetListAsync();
            var dtos = _mapper.Map<List<ResultBlogCategoryDTO>>(blogCategoryList);
            return Ok(dtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {

            var blogCategories = await _blogCategoryService.GetByIdAsync(id);
            if (blogCategories == null) return NotFound();
            var dtos = _mapper.Map<ResultBlogCategoryDTO>(blogCategories);
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogCategoryDTO createBlogCategoryDTO)
        {
            var dtos= _mapper.Map<BlogCategory>(createBlogCategoryDTO);
            await _blogCategoryService.CreateAsync(dtos);
            return Ok("Yeni Kategori Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateBlogCategoryDTO updateBlogCategoryDTO)
        {
            var dtos = _mapper.Map<BlogCategory>(updateBlogCategoryDTO);
            await _blogCategoryService.UpdateAsync(dtos);
            return Ok("Kategori Alanı Güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blogCategories = await _blogCategoryService.GetByIdAsync(id);
            if (blogCategories == null)
                return NotFound();

            await _blogCategoryService.DeleteAsync(blogCategories);
            return Ok();
        }

    }
}
