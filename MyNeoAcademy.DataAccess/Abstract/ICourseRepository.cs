using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface ICourseRepository:IRepository<Course>
    {
        //Özel Metotlar
        Task<List<Course>> GetAllWithCategoryAndInstructorAsync();
        Task<Course?> GetByIdWithCategoryAndInstructorAsync(int id);
    }
}
