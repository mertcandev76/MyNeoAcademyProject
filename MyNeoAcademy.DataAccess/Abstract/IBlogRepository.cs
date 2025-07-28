using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IBlogRepository : IRepository<Blog>
    {
            Task<List<Blog>> GetAllWithIncludesAsync();
            Task<Blog?> GetByIdWithIncludesAsync(int id);

    }
}
