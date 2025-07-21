using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class BlogTagManager : GenericManager<BlogTag>, IBlogTagService
    {
        private readonly IBlogTagRepository _blogTagRepository;

        public BlogTagManager(IBlogTagRepository blogTagRepository):base(blogTagRepository) 
        {
            _blogTagRepository = blogTagRepository;
        }

        public async Task<List<BlogTag>> GetAllWithIncludesAsync() =>await _blogTagRepository.GetAllWithIncludesAsync();

        public async Task<BlogTag?> GetByIdWithIncludesAsync(int id) => await _blogTagRepository.GetByIdWithIncludesAsync(id);
        public async Task<bool> ExistsAsync(int blogId, int tagId) => await _blogTagRepository.ExistsAsync(blogId, tagId);
    }
}
