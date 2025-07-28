using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    namespace MyNeoAcademy.Application.Abstract
    {
        public interface IInstructorService : IGenericService<
            Instructor,
            CreateInstructorDTO,
            UpdateInstructorDTO,
            ResultInstructorDTO
        >
        {

            Task<List<ResultInstructorDTO>> GetAllWithIncludesAsync();
            Task<ResultInstructorDTO?> GetByIdWithIncludesAsync(int id);
            Task CreateWithFileAsync(CreateInstructorWithFileDTO dto, string webRootPath);
            Task UpdateWithFileAsync(UpdateInstructorWithFileDTO dto, string webRootPath);
            Task<bool> DeleteByIdAsync(int id);
        }
    }


}
