using Application.Interfaces;
using Application.Interfaces.UnitOfWorks;
using Core.Entities;
using Core.Entities.Core.Entities;
using Infrastructure.Data;

using Persistence.Repository;


namespace Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<User> _users;
        private IRepository<Request> _requests;
        private IRepository<Actions> _actions;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users => _users ??= new Repository<User>(_context);
        public IRepository<Request> Requests => _requests ??= new Repository<Request>(_context);
        public IRepository<Actions> Actions => _actions ??= new Repository<Actions>(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // IDisposable implementasyonu ile kaynakları serbest bırak
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
