using System;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
    //Repository<T>: Bu sınıf, generic bir yapı kullanır.
    //T, sınıfın çalışacağı veri modelinin türünü temsil eder. 
    public class Repository<T> : IRepository<T> where T : class

    {

        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        //_dbSet: DbSet<T> türünde bir alan.
        //Bu, DbContext'ten belirli bir modelin (örneğin, Request veya Action) veritabanındaki temsilini alır.

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        //Verilen bir T türü nesnesini (örneğin, bir yeni Request kaydı) veritabanına ekler.
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }


        // Verilen bir T türü nesnesini veritabanından siler.
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }


        // Veritabanındaki T türündeki tüm kayıtları döner.
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }


        //Verilen bir id'ye göre T türünden bir kaydı döner.
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }



        //Verilen bir T türü nesnesini günceller.
        public void Update(T entity)
        {
           _dbSet.Update(entity);

        }

    }

}


