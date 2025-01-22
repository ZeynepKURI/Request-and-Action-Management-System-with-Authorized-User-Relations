using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    // IRepository<T>: Generic repository arayüzü, burada T veri modelinin türünü temsil eder.
    // where T : class: Generic tür T, yalnızca sınıf (referans türü) olabilir.
    public interface IRepository<T> where T : class
    {
        // Asenkron olarak bir T türü nesnesini veritabanına ekler.
        Task<T> AddAsync(T entity);

        // Asenkron olarak bir T türü nesnesini veritabanında günceller.
        Task<T> UpdateAsync(T entity);

        // Asenkron olarak verilen id'ye sahip bir T türü nesnesini veritabanından siler.
        Task<bool> DeleteAsync(int id);

        // Asenkron olarak verilen id'ye sahip bir T türü nesnesini alır.
        Task<T> GetByIdAsync(int id);

        // Asenkron olarak veritabanındaki tüm T türündeki nesneleri alır.
        Task<IEnumerable<T>> GetAllAsync();
    }
}
