using AutoMapper;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
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
    public class CategoryManager : GenericManager<Category, CreateCategoryDTO, UpdateCategoryDTO, ResultCategoryDTO>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
            : base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
        }



        public async Task<List<ResultCategoryDTO>> GetAllWithIncludesAsync()
        {
            var categories = await _categoryRepository.GetAllWithIncludesAsync();
            return _mapper.Map<List<ResultCategoryDTO>>(categories);
        }

        public async Task<ResultCategoryDTO?> GetByIdWithIncludesAsync(int id)
        {
            var category = await _categoryRepository.GetByIdWithIncludesAsync(id);
            return _mapper.Map<ResultCategoryDTO>(category);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return false;

            await _categoryRepository.DeleteAsync(category);
            return true;
        }
    }
}
