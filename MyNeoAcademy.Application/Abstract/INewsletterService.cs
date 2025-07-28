using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface INewsletterService : IGenericService<
       Newsletter,
       CreateNewsletterDTO,
       UpdateNewsletterDTO,
       ResultNewsletterDTO>
    {
        Task<bool> DeleteByIdAsync(int id);
    }
}
