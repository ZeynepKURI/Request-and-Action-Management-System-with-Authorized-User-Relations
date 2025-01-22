using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Persistence.Context;

namespace Persistence.Repository
{
    // Repository<T>: Bu sınıf, generic bir yapı kullanır.
    // T, sınıfın çalışacağı veri modelinin türünü temsil eder.
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        // Repository<T> sınıfının constructor'ı, DbContext ile DbSet'i başlatır.
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Verilen bir T türü nesnesini (örneğin, yeni bir Request kaydı) veritabanına asenkron şekilde ekler.
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydeder.
            return entity;
        }

        // Verilen bir T türü nesnesini asenkron şekilde veritabanından siler.
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydeder.
                return true;
            }
            return false;
        }

        // Veritabanındaki T türündeki tüm kayıtları asenkron şekilde döner.
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Verilen bir id'ye göre T türünden bir kaydı asenkron şekilde döner.
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Verilen bir T türü nesnesini asenkron şekilde günceller.
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydeder.
            return entity;
        }
    }
}
