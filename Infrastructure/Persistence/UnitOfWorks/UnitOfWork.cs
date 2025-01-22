using Application.Interfaces;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Persistence.Context;
using Persistence.Repository;
using System;

namespace Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private Repository<Request> _requests;
        private Repository<Actions> _actions;

        // Tüm repositorylerin lazily load edilmesi için ctor
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Repository'ler tembel yükleme ile oluşturuluyor
        public IRepository<Request> Requests => _requests ??= new Repository<Request>(_context);

        public IRepository<Actions> Actions => _actions ??= new Repository<Actions>(_context);

        // Veritabanındaki değişiklikleri kaydet
        public void Commit()
        {
            _context.SaveChanges();
        }

        // IDisposable implementasyonu ile kaynakları serbest bırak
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
