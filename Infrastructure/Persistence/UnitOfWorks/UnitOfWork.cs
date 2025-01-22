using System;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Persistence.Context;
using Persistence.Repository;

namespace Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Repository <Request> _requests;
        private Repository<Action> _actions;
        public UnitOfWork( AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Request> Requests => _requests ??= new Repository<Request>(_context);

        public IRepository<Action> Actions => _actions ??= new Repository<Action>(_context);                  



        public void Commit() //Birden fazla repository ile çalışıyorsanız, bu metot tüm değişiklikleri tek bir noktada kaydetmenizi sağlar.
        {
            _context.SaveChanges(); // Değişiklikleri kaydet
        }

        public void Dispose()
        {
            _context.Dispose();  //Dispose() metodu çağrıldığında, açık olan veritabanı bağlantısı kapatılır ve tüm kaynaklar temizlenir.
        }
    }
}

