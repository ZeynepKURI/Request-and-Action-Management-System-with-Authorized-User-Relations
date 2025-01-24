using Application.Interfaces.Repository;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repository
{

    // Generic bir repository, T tipi ile çalışır ve IRepository<T> arayüzünü uygular
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context; // Veritabanı bağlamı
        private readonly DbSet<T> _dbSet; // Veritabanında ilgili tabloya karşılık gelen DbSet

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>(); // T tipi DbSet olarak ayarlanır
        }

        // ID ile bir varlık getirir
        public async Task<T> GetByIdAsync(int id) =>

            await _dbSet.FindAsync(id);


        // Tablodaki tüm verileri getirir
        public async Task<IEnumerable<T>> GetAllAsync() =>

            await _dbSet.ToListAsync();



        // Yeni bir varlık ekler
        public async Task AddAsync(T entity) =>

            await _dbSet.AddAsync(entity);


        // Mevcut bir varlığı günceller
        public void Update(T entity) =>

            _dbSet.Update(entity);



        // Bir varlığı siler
        public void Delete(T entity) =>

            _dbSet.Remove(entity);



        // Değişiklikleri veritabanına kaydeder
        public async Task SaveChangesAsync()

            => await _context.SaveChangesAsync();
    }

}
