using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ITagService : IGenericService<
        Tag,
        CreateTagDTO,
        UpdateTagDTO,
        ResultTagDTO>
    {

        Task<List<ResultTagDTO>> GetAllWithIncludesAsync();
        Task<ResultTagDTO?> GetByIdWithIncludesAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
    }
}
