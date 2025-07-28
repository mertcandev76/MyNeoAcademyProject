using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
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
    public class AboutFeatureManager : GenericManager<
          AboutFeature,
          CreateAboutFeatureDTO,
          UpdateAboutFeatureDTO,
          ResultAboutFeatureDTO>, IAboutFeatureService
    {
        private readonly IAboutFeatureRepository _aboutFeatureRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AboutFeatureManager(
            IAboutFeatureRepository aboutFeatureRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
            : base(aboutFeatureRepository, mapper)
        {
            _aboutFeatureRepository = aboutFeatureRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ResultAboutFeatureDTO>> GetAllWithIncludesAsync()
        {
            var aboutFeatures = await _aboutFeatureRepository.GetAllWithIncludesAsync();
            var result = _mapper.Map<List<ResultAboutFeatureDTO>>(aboutFeatures);

            return result;
        }

        public async Task<ResultAboutFeatureDTO?> GetByIdWithIncludesAsync(int id)
        {
            var aboutFeature = await _aboutFeatureRepository.GetByIdWithIncludesAsync(id);
            var result = _mapper.Map<ResultAboutFeatureDTO>(aboutFeature);

            return result;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _aboutFeatureRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _aboutFeatureRepository.DeleteAsync(entity);
            return true;
        }
    }
}
