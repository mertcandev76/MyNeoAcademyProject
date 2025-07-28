using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IAuthorService : IGenericService<
       Author,                 
       CreateAuthorDTO,        
       UpdateAuthorDTO,        
       ResultAuthorDTO         
   >
    {
        Task<List<ResultAuthorDTO>> GetAllWithIncludesAsync();
        Task<ResultAuthorDTO?> GetByIdWithIncludesAsync(int id);
        Task CreateWithFileAsync(CreateAuthorWithFileDTO dto, string webRootPath);
        Task UpdateWithFileAsync(UpdateAuthorWithFileDTO dto, string webRootPath);
        Task<bool> DeleteByIdAsync(int id);

    }



}
