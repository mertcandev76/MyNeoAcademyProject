using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IAboutFeatureRepository : IRepository<AboutFeature>
    {
        //Özel Metotlar
        Task<List<AboutFeature>> GetAllWithAboutAsync();
        Task<AboutFeature?> GetByIdWithBlogAboutAsync(int id);
    }
}
