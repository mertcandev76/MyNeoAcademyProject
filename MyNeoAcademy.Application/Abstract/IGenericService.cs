using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IGenericService<T> where T : class
    {
        //Tüm verileri getirir
        Task<List<T>> GetListAsync();
        //Verilen id'ye sahip olan nesneyi getirir.
        Task<T?> GetByIdAsync(int id);
        //Yeni bir nesneyi veritabanına ekler.
        Task CreateAsync(T entity);
        //Var olan nesneyi günceller.
        Task UpdateAsync(T entity);
        //ID ile eşleşen kaydı siler.
        Task DeleteAsync(T entity);

        //Toplam kayıt sayısını döner.
        Task<int> CountAsync();

        //Belirli bir filtreye göre kayıt sayısını döner.
        Task<int> FilteredCountAsync(Expression<Func<T, bool>> predicate);

        //Belirli bir filtreye göre liste döner.
        Task<List<T>> GetFilteredListAsync(Expression<Func<T, bool>> predicate);

        //Belirli bir şarta uyan ilk nesneyi döner.
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate);

    }
}
