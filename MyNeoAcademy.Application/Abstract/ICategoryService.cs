using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ICategoryService : IGenericService<
       Category,
       CreateCategoryDTO,
       UpdateCategoryDTO,
       ResultCategoryDTO
   >
    {
        Task<List<ResultCategoryDTO>> GetAllWithIncludesAsync();
        Task<ResultCategoryDTO?> GetByIdWithIncludesAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
    }
}
