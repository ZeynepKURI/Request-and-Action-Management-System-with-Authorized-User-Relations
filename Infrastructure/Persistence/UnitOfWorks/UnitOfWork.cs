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
        private readonly AppDbContext _context;
        private IRepository<Request> _requests;
        private IRepository<Actions> _actions;
        private IRepository<User> _users;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Request> Requests => _requests ??= new Repository<Request>(_context);
        public IRepository<Actions> Actions => _actions ??= new Repository<Actions>(_context);
        public IRepository<User> Users => _users ??= new Repository<User>(_context);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();


        public void Dispose() =>
            _context.Dispose();
    }


}
