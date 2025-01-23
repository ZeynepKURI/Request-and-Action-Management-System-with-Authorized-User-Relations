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
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
