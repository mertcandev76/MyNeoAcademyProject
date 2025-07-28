using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IAboutService : IGenericService<
       About,                 
       CreateAboutDTO,        
       UpdateAboutDTO,        
       ResultAboutDTO         
   >
    {
        //Özel Metotlar

        Task<List<ResultAboutDTO>> GetAllWithIncludesAsync();
        Task<ResultAboutDTO?> GetByIdWithIncludesAsync(int id);
        Task CreateWithFileAsync(CreateAboutWithFileDTO dto, string webRootPath);
        Task UpdateWithFileAsync(UpdateAboutWithFileDTO dto, string webRootPath);
        Task<bool> DeleteByIdAsync(int id);
    }
}

