using System;
using Application.DTOs;

namespace Application.Interfaces.Service
{
  public interface IRequestService
        {
            Task<IEnumerable<RequestDto>> GetAllRequestsAsync();
            Task<RequestDto> GetRequestByIdAsync(int id);
            Task CreateRequestAsync(RequestDto requestDto);
            Task UpdateRequestAsync(int id, RequestDto requestDto);
            Task DeleteRequestAsync(int id);
            Task UpdateRequestStatusAsync(int id, string status);
        }
    }



