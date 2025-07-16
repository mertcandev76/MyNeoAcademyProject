using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.BlogTagDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class BlogTagManager:GenericManager<BlogTag>,IBlogTagService
    {
        private readonly IBlogTagRepository _blogTagRepository;

        public BlogTagManager(IBlogTagRepository blogTagRepository):base(blogTagRepository) 
        {
            _blogTagRepository = blogTagRepository;
        }

        public async Task<List<BlogTag>> GetAllWithBlogAndTagAsync()=>await _blogTagRepository.GetAllWithBlogAndTagAsync();

        public async Task<BlogTag?> GetByIdWithBlogAndTagAsync(int id) => await _blogTagRepository.GetByIdWithBlogAndTagAsync(id);
    }
}
