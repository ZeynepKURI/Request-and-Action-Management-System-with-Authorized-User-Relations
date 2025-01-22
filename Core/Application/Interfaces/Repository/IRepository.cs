using System;
namespace Application.Interfaces
{
	public interface IRepository<T>   //IRepository<T>: Bu sınıf, generic bir yapı kullanır.
                                      //T, sınıfın çalışacağı veri modelinin türünü temsil eder. 

        where T : class      //where T : class: Generic tür T, yalnızca bir sınıf (referans türü) olabilir.                

    {
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		T GetById(int id);
		IEnumerable<T> GetAll();
	}
}

