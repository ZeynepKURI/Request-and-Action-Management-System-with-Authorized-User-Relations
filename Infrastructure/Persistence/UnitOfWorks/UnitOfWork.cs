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

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

