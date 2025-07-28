using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        Task<List<Instructor>> GetAllWithIncludesAsync();
        Task<Instructor?> GetByIdWithIncludesAsync(int id);

    }
}
