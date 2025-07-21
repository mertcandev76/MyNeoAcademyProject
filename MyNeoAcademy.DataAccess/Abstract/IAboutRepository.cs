using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IAboutRepository:IRepository<About>
    {
        //Özel Metotlar
        Task<About?> GetAllWithAboutFeatureAsync(int id);
    }
}
