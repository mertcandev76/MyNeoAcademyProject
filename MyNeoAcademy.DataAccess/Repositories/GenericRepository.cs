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
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly MyNeoAcademyContext _myNeoAcademyContext;

        public GenericRepository(MyNeoAcademyContext myNeoAcademyContext)
        {
            _myNeoAcademyContext = myNeoAcademyContext;
        }
        public DbSet<T> Table { get => _myNeoAcademyContext.Set<T>(); }

        //Tüm verileri getirir
        public async Task<List<T>> GetListAsync()
        {
            return await Table.ToListAsync();
        }
        //Verilen id'ye sahip olan nesneyi getirir.
        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }
        //Yeni bir nesneyi veritabanına ekler.
        public async Task CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
            await _myNeoAcademyContext.SaveChangesAsync();
        }
        //Var olan nesneyi günceller.
        public async Task UpdateAsync(T entity)
        {
            Table.Update(entity);
            await _myNeoAcademyContext.SaveChangesAsync();
        }
        //ID ile eşleşen kaydı siler.
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                Table.Remove(entity);
                await _myNeoAcademyContext.SaveChangesAsync();
            }
        }
        //Toplam kayıt sayısını döner.
        public async Task<int> CountAsync()
        {
            return await Table.CountAsync();
        }
        //Belirli bir filtreye göre kayıt sayısını döner.
        public async Task<int> FilteredCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.CountAsync(predicate);
        }
        //Belirli bir filtreye göre liste döner.
        public async Task<List<T>> GetFilteredListAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }
        //Belirli bir şarta uyan ilk nesneyi döner.
        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }
    }
}
