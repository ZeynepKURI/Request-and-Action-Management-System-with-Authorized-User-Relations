using Application.DTOs;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Persistence.Services
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RequestService> _logger;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RequestService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RequestDto>> GetAllRequestsAsync()
        {
            var requests = await _unitOfWork.Requests.GetAllAsync();
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        public async Task<RequestDto> GetRequestByIdAsync(int id)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                _logger.LogWarning($"Request with ID {id} not found.");
                throw new Exception("Request not found.");
            }

            return _mapper.Map<RequestDto>(request);
        }

        public async Task CreateRequestAsync(RequestDto requestDto)
        {
            var request = _mapper.Map<Request>(requestDto);
            request.CreatedAt = DateTime.Now;

            await _unitOfWork.Requests.AddAsync(request);
            _unitOfWork.Commit();
        }

        public async Task UpdateRequestAsync(int id, RequestDto requestDto)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found.");
            }

            _mapper.Map(requestDto, request);
            request.UpdatedAt = DateTime.Now;

            await _unitOfWork.Requests.UpdateAsync(request);
            _unitOfWork.Commit();
        }

        public async Task DeleteRequestAsync(int id)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found.");
            }

            await _unitOfWork.Requests.DeleteAsync(id);
            _unitOfWork.Commit(); // Commit all changes in one transaction
        }

        public async Task UpdateRequestStatusAsync(int id, string status)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found.");
            }

            request.Status = status;
            await _unitOfWork.Requests.UpdateAsync(request);
            _unitOfWork.Commit();
        }
    }
}
