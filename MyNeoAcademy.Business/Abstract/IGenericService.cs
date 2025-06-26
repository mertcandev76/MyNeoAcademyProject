using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        //Tüm verileri getirir
        Task<List<T>> TGetListAsync();
        //Verilen id'ye sahip olan nesneyi getirir.
        Task<T> TGetByIdAsync(int id);
        //Yeni bir nesneyi veritabanına ekler.
        Task TCreateAsync(T entity);
        //Var olan nesneyi günceller.
        Task TUpdateAsync(T entity);
        //ID ile eşleşen kaydı siler.
        Task TDeleteAsync(int id);
        //Toplam kayıt sayısını döner.
        Task<int> TCountAsync();
        //Belirli bir filtreye göre kayıt sayısını döner.
        Task<int> TFilteredCountAsync(Expression<Func<T, bool>> predicate);
        //Belirli bir filtreye göre liste döner.
        Task<List<T>> TGetFilteredListAsync(Expression<Func<T, bool>> predicate);
        //Belirli bir şarta uyan ilk nesneyi döner.
        Task<T> TGetByFilterAsync(Expression<Func<T, bool>> predicate);
    }
}
