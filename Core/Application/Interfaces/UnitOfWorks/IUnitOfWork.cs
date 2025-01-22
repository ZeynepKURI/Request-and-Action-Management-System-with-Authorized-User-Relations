using System;
using Domain.Entities;

namespace Application.Interfaces.UnitOfWorks
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository <Request> Requests { get; }

		IRepository< Actions> Actions { get; }

		void Commit();
	}
}

