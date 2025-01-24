
namespace Application.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {

        // T türünün bir sınıf olmasını zorunlu kılar

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
