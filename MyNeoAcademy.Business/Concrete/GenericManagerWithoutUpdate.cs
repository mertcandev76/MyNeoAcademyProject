using AutoMapper;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class GenericManagerWithoutUpdate<TEntity, TCreateDto, TResultDto>
    : IGenericServiceWithoutUpdate<TEntity, TCreateDto, TResultDto>
    where TEntity : class
    where TCreateDto : class
    where TResultDto : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericManagerWithoutUpdate(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TResultDto>> GetListAsync()
        {
            var entities = await _repository.GetListAsync();
            return _mapper.Map<List<TResultDto>>(entities);
        }

        public async Task<TResultDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TResultDto>(entity);
        }

        public virtual async Task CreateAsync(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Entity not found");

            await _repository.DeleteAsync(entity);
        }

        public async Task<int> CountAsync() => await _repository.CountAsync();

        public async Task<List<TResultDto>> GetFilteredListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _repository.GetFilteredListAsync(predicate);
            return _mapper.Map<List<TResultDto>>(entities);
        }

        public async Task<TResultDto?> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await _repository.GetByFilterAsync(predicate);
            return _mapper.Map<TResultDto>(entity);
        }

        public async Task<int> FilteredCountAsync(Expression<Func<TEntity, bool>> predicate)
            => await _repository.FilteredCountAsync(predicate);
    }

}
