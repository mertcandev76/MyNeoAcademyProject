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
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository):base(categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }


        public async Task<List<Category>> GetAllWithBlogAsync()=>await _categoryRepository.GetAllWithBlogAsync();

        public async Task<Category?> GetByIdWithBlogAsync(int id) => await _categoryRepository.GetByIdWithBlogAsync(id);
    }
}
