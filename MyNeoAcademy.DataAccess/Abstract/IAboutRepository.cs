using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IAboutRepository:IRepository<About>
    {
        Task<List<About>> GetAllWithIncludesAsync();
        Task<About?> GetByIdWithIncludesAsync(int id);
    }
}
