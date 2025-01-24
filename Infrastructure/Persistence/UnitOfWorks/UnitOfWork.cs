using System;
using System.Threading.Tasks;
using Application.Interfaces.Repository;
using Application.Interfaces.UnitOfWorks;
using Core.Entities;

using Infrastructure.Data;
using Persistence.Repository;

namespace Persistence.UnitOfWorks
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context; // Veritabanı bağlamı
        private IRepository<Request> _requests; // Request repository'si
        private IRepository<Actions> _actions; // Action repository'si
        private IRepository<User> _users; // User repository'si


        // Constructor, AppDbContext'i alır ve bağımlılıkları enjekte eder
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Requests repository'sine erişim sağlar
        public IRepository<Request> Requests => _requests ??= new Repository<Request>(_context);

        // Actions repository'sine erişim sağlar
        public IRepository<Actions> Actions => _actions ??= new Repository<Actions>(_context);

        // Users repository'sine erişim sağlar
        public IRepository<User> Users => _users ??= new Repository<User>(_context);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();


        // Unit of Work kullanımı bittiğinde veritabanı bağlamını serbest bırakır
        public void Dispose() =>
            _context.Dispose();
    }


}
