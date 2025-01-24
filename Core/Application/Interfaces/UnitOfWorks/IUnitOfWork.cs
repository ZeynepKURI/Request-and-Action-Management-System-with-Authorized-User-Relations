
using Application.Interfaces.Repository;
using Core.Entities;


namespace Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Request> Requests { get; }
        IRepository<Actions> Actions { get; }
        IRepository<User> Users { get; }
        Task SaveChangesAsync();
    }


}