using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IAboutDetailService : IGenericService<
        AboutDetail,
        CreateAboutDetailDTO,
        UpdateAboutDetailDTO,
        ResultAboutDetailDTO>
    {
        Task<bool> DeleteByIdAsync(int id);
    }
}