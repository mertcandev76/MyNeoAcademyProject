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

        Task<List<Course>> GetAllWithIncludesAsync();
        Task<Course?> GetByIdWithIncludesAsync(int id);
    }
}
