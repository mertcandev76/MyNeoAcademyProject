using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class AboutFeatureManager : GenericManager<AboutFeature>, IAboutFeatureService
    {
        private readonly IAboutFeatureRepository _aboutFeatureRepository;

        public AboutFeatureManager(IAboutFeatureRepository aboutFeatureRepository):base(aboutFeatureRepository) 
        {
            _aboutFeatureRepository = aboutFeatureRepository;
        }

        public async Task<List<AboutFeature>> GetAllWithAboutAsync() => await _aboutFeatureRepository.GetAllWithAboutAsync();

        public async Task<AboutFeature?> GetByIdWithBlogAboutAsync(int id) => await _aboutFeatureRepository.GetByIdWithBlogAboutAsync(id);
    }
}
