
using Core.Entities;
using Core.Entities.Core.Entities;

namespace Application.Interfaces.UnitOfWorks
{
	public interface IUnitOfWork : IDisposable
	{
        IRepository<User> Users { get; }
        IRepository<Request> Requests { get; }
        IRepository<Actions> Actions { get; }

        void Commit();
        Task SaveAsync();
    }
}

