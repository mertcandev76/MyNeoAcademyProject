using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogManager(IBlogRepository blogRepository):base(blogRepository) 
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<Blog>> GetAllWithIncludesAsync()=> await _blogRepository.GetAllWithIncludesAsync();

        public async Task<Blog?> GetByIdWithIncludesAsync(int id) => await _blogRepository.GetByIdWithIncludesAsync(id);
    }
}
