using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IGenericServiceWithoutUpdate<TEntity, TCreateDto, TResultDto>
     where TEntity : class
     where TCreateDto : class
     where TResultDto : class
    {
        Task<List<TResultDto>> GetListAsync();
        Task<TResultDto?> GetByIdAsync(int id);
        Task CreateAsync(TCreateDto dto);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
        Task<List<TResultDto>> GetFilteredListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TResultDto?> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> FilteredCountAsync(Expression<Func<TEntity, bool>> predicate);
    }

}
