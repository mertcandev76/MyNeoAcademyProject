using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ISliderService : IGenericService<
         Slider,
         CreateSliderDTO,
         UpdateSliderDTO,
         ResultSliderDTO>
    {
        Task CreateWithFileAsync(CreateSliderWithFileDTO dto, string webRootPath);
        Task UpdateWithFileAsync(UpdateSliderWithFileDTO dto, string webRootPath);
        Task<bool> DeleteByIdAsync(int id);
    }
}
