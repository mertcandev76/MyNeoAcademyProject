using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ITagService : IGenericService<Tag>
    {
        Task<List<Tag>> GetAllWithIncludesAsync();
        Task<Tag?> GetByIdWithIncludesAsync(int id);
    }
}
