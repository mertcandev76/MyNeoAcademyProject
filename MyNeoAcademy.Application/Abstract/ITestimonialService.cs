using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ITestimonialService : IGenericService<
       Testimonial,
       CreateTestimonialDTO,
       UpdateTestimonialDTO,
       ResultTestimonialDTO
   >
    {
        Task CreateWithFileAsync(CreateTestimonialWithFileDTO dto, string webRootPath);
        Task UpdateWithFileAsync(UpdateTestimonialWithFileDTO dto, string webRootPath);
        Task<bool> DeleteByIdAsync(int id);
    }
}
