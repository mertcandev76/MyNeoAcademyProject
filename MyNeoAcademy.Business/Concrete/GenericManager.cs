using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {

        private readonly IRepository<T> _repository;

        public GenericManager(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<List<T>> GetListAsync() =>  await _repository.GetListAsync();

        public async Task<T> GetByIdAsync(int id) =>   await _repository.GetByIdAsync(id);

        public async Task CreateAsync(T entity) => await _repository.CreateAsync(entity);

        public async Task UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(T entity) => await _repository.DeleteAsync(entity);

        public async Task<int> CountAsync() =>    await _repository.CountAsync();

        public async Task<int> FilteredCountAsync(Expression<Func<T, bool>> predicate) =>  await _repository.FilteredCountAsync(predicate);

        public async Task<List<T>> GetFilteredListAsync(Expression<Func<T, bool>> predicate) =>   await _repository.GetFilteredListAsync(predicate);

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> predicate) =>   await _repository.GetByFilterAsync(predicate);
    }
}
