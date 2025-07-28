using MyNeoAcademy.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IGenericService<TEntity, TCreateDto, TUpdateDto, TResultDto>
        where TEntity : class
        where TCreateDto : class
        where TUpdateDto : class, IHasId
        where TResultDto : class
    {
        // Listeleme ve Detay için
        Task<List<TResultDto>> GetListAsync();
        Task<TResultDto?> GetByIdAsync(int id);

        // Oluşturma ve Güncelleme için
        Task CreateAsync(TCreateDto dto);
        Task UpdateAsync(TUpdateDto dto);

        // Silme için
        Task DeleteAsync(int id);

        // Sayım
        Task<int> CountAsync();

        // (Opsiyonel) Gelişmiş filtreleme – DTO değil, Entity üzerinden tanımlanmalı!
        Task<List<TResultDto>> GetFilteredListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TResultDto?> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> FilteredCountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}