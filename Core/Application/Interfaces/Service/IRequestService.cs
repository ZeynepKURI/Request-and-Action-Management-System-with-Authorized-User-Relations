using System;
using Application.DTOs;

namespace Application.Interfaces.Service
{
    public interface IRequestService
    {
        Task<RequestDto> GetRequestByIdAsync(int id);
        Task<IEnumerable<RequestDto>> GetRequestsByUserIdAsync(int userId);
        Task<IEnumerable<RequestDto>> GetAllRequestsAsync();
        Task AddRequestAsync(RequestDto dto);
        Task UpdateRequestAsync(int id, RequestDto dto);
        Task DeleteRequestAsync(int id);
    }

}

