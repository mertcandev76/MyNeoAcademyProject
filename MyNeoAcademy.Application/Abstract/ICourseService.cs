using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ICourseService : IGenericService<
       Course,
       CreateCourseDTO,
       UpdateCourseDTO,
       ResultCourseDTO>
    {


        Task<List<ResultCourseDTO>> GetAllWithIncludesAsync();

        Task<ResultCourseDTO?> GetByIdWithIncludesAsync(int id);

        Task CreateWithFileAsync(CreateCourseWithFileDTO dto, string webRootPath);

        Task UpdateWithFileAsync(UpdateCourseWithFileDTO dto, string webRootPath);

        Task<bool> DeleteByIdAsync(int id);
    }
}
