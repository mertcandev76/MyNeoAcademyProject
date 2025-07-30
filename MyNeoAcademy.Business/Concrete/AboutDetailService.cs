using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class AboutDetailManager : GenericManager<AboutDetail, CreateAboutDetailDTO, UpdateAboutDetailDTO, ResultAboutDetailDTO>, IAboutDetailService
    {
        private readonly IRepository<AboutDetail> _aboutDetailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AboutDetailManager(
            IRepository<AboutDetail> aboutDetailRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        ) : base(aboutDetailRepository, mapper)
        {
            _aboutDetailRepository = aboutDetailRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _aboutDetailRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _aboutDetailRepository.DeleteAsync(entity);
            return true;
        }

        public override async Task CreateAsync(CreateAboutDetailDTO dto)
        {
            var entity = _mapper.Map<AboutDetail>(dto);
            await _aboutDetailRepository.CreateAsync(entity);
        }

        public override async Task<List<ResultAboutDetailDTO>> GetListAsync()
        {
            var entities = await _aboutDetailRepository.GetListAsync();
            return _mapper.Map<List<ResultAboutDetailDTO>>(entities);
        }

        public override async Task<ResultAboutDetailDTO?> GetByIdAsync(int id)
        {
            var entity = await _aboutDetailRepository.GetByIdAsync(id);
            return _mapper.Map<ResultAboutDetailDTO>(entity);
        }
    }
}
