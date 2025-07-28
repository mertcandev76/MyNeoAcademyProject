using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MyNeoAcademyContext _myNeoAcademyContext;

        public GenericRepository(MyNeoAcademyContext myNeoAcademyContext)
        {
            _myNeoAcademyContext = myNeoAcademyContext;
        }
        public DbSet<TEntity> Table { get => _myNeoAcademyContext.Set<TEntity>(); }


        public async Task<List<TEntity>> GetListAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            await _myNeoAcademyContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Table.Update(entity);
            await _myNeoAcademyContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Table.Remove(entity);
            await _myNeoAcademyContext.SaveChangesAsync();
        }


        public async Task<int> CountAsync()
        {
            return await Table.CountAsync();
        }

        public async Task<int> FilteredCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.CountAsync(predicate);
        }

        public async Task<List<TEntity>> GetFilteredListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }
    }
}
