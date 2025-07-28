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
    public class NewsletterManager : GenericManager<Newsletter, CreateNewsletterDTO, UpdateNewsletterDTO, ResultNewsletterDTO>, INewsletterService
    {
        private readonly IRepository<Newsletter> _newsletterRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewsletterManager(
            IRepository<Newsletter> newsletterRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        ) : base(newsletterRepository, mapper)
        {
            _newsletterRepository = newsletterRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _newsletterRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _newsletterRepository.DeleteAsync(entity);
            return true;
        }
        public override async Task CreateAsync(CreateNewsletterDTO dto)
        {
            var entity = _mapper.Map<Newsletter>(dto);
            entity.SubscribedDate = DateTime.UtcNow;  

            await _newsletterRepository.CreateAsync(entity);  
        }
        public override async Task<List<ResultNewsletterDTO>> GetListAsync()
        {
            var newsletters = await _newsletterRepository.GetListAsync();
            return _mapper.Map<List<ResultNewsletterDTO>>(newsletters);
        }

        public override async Task<ResultNewsletterDTO?> GetByIdAsync(int id)
        {
            var entity = await _newsletterRepository.GetByIdAsync(id);
            return _mapper.Map<ResultNewsletterDTO>(entity);
        }
    }
}
