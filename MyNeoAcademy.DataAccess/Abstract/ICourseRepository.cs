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
        Task<List<Course>> GetAllWithCategoryAsync();
        Task<Course?> GetByIdWithCategoryAsync(int id);
    }
}
