using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ICourseService:IGenericService<Course>
    {
        //Özel Metotlar
        Task<List<Course>> GetAllWithCategoryAndInstructorAsync();
        Task<Course?> GetByIdWithCategoryAndInstructorAsync(int id);
    }
}
