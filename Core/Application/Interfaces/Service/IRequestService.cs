using System;
using Application.DTOs;

namespace Application.Interfaces.Service
{
	public interface IRequestService
	{
        Task<IEnumerable<RequestDto>> GetAllRequestsAsync(int userId);  //IEnumerable<T> bir koleksiyon tipi arayüzüdür ve veri kümelerinin sıralanabilir olduğunu belirtmek için kullanılır. 
        Task<RequestDto> GetRequestByIdAsync(int id, int userId);
        Task CreateRequestAsync(RequestDto requestDto);
        Task UpdateRequestAsync(int id, RequestDto requestDto);
        Task DeleteRequestAsync(int id, int userId);


    }
}

