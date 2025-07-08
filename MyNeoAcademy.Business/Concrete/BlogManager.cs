using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class BlogManager:GenericManager<Blog>,IBlogService
    {
        private readonly IBlogRepository _repository;

        public BlogManager(IBlogRepository repository):base(repository) 
        {
            _repository = repository;
        }

        public Task<List<Blog>> GetAllWithBlogCategoryAsync() => _repository.GetAllWithBlogCategoryAsync();

        public Task<Blog?> GetByIdWithBlogCategoryAsync(int id) => _repository.GetByIdWithBlogCategoryAsync(id);
    }
}
