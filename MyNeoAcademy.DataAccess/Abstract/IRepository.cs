using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {

        Task<List<TEntity>> GetListAsync();

        Task<TEntity?> GetByIdAsync(int id);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);


        Task<int> CountAsync();


        Task<int> FilteredCountAsync(Expression<Func<TEntity, bool>> predicate);


        Task<List<TEntity>> GetFilteredListAsync(Expression<Func<TEntity, bool>> predicate);


        Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
