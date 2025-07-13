using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Abstract
{
    public interface IAboutFeatureService : IGenericService<AboutFeature>
    {
        //Özel Metotlar
        Task<List<AboutFeature>> GetAllWithAboutAsync();
        Task<AboutFeature?> GetByIdWithBlogAboutAsync(int id);
    }
}
