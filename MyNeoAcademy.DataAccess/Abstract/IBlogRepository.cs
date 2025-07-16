using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IBlogRepository:IRepository<Blog>
    {
        //Özel Metotlar
        Task<List<Blog>> GetAllWithCategoryAndAuthorAsync();
        Task<Blog?> GetByIdWithCategoryAndAuthorAsync(int id);
    }
}
