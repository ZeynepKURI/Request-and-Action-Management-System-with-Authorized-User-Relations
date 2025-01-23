
using Application.DTOs;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;


namespace Persistence.Service
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequestDto>> GetAllRequestsAsync(int userId)
        {
            var requests = await _unitOfWork.Requests.GetAllAsync();

            // Sadece userId'ye ait talepleri filtrele ve DTO'ya map et
            var filteredRequests = requests
                .Where(r => r.UserId == userId)
                .ToList();

            return _mapper.Map<IEnumerable<RequestDto>>(filteredRequests);

        }

        public async Task<RequestDto> GetRequestByIdAsync(int id, int userId)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found");
            }

            if (request.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to view this request.");
            }

            return _mapper.Map<RequestDto>(request);

        }

        public async Task CreateRequestAsync(RequestDto requestDto)
        {
            var request = _mapper.Map<Request>(requestDto);

            await _unitOfWork.Requests.AddAsync(request);

            // Action'lar eklenirken Request ID atanıyor
            var actions = requestDto.actions.Select(actionDto => new Actions
            {
                ActionDescription = actionDto.ActionDescription,
                RequestId = request.Id
            });
        }

        public async Task UpdateRequestAsync(int id, RequestDto requestDto)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found");
            }

            if (request.UserId != requestDto.UserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this request.");
            }

            // Mevcut Request güncelleniyor
            _mapper.Map(requestDto, request);

            await _unitOfWork.Requests.UpdateAsync(request);

            // Actions güncelleniyor
            foreach (var actionDto in requestDto.actions)
            {
                var action = await _unitOfWork.Actions.GetByIdAsync(actionDto.Id);
                if (action != null)
                {
                    _mapper.Map(actionDto, action);
                    await _unitOfWork.Actions.UpdateAsync(action);
                }
            }

            _unitOfWork.Commit();
        }


        public async Task DeleteRequestAsync(int id, int userId)
        {

            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found");
            }

            if (request.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this request.");
            }

            await _unitOfWork.Requests.DeleteAsync(id);

            foreach (var action in request.actions)
            {
                await _unitOfWork.Actions.DeleteAsync(action.Id);
            }

            _unitOfWork.Commit();
        }
    }

}

