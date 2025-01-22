
using Application.DTOs;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;


namespace Persistence.Service
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RequestDto>> GetAllRequestsAsync(int userId)
        {
            // Sadece verilen userId'ye ait talepler alınır.
            var requests = await _unitOfWork.Requests.GetAllAsync();
            return requests
                .Where(r => r.UserId == userId) // Kullanıcının talepleri
                .Select(r => new RequestDto
                {
                    Title = r.Title,
                    Description = r.Description,
                    UserId = r.UserId,
                    actions = r.actions.Select(a => new ActionDto { ActionDescription = a.ActionDescription }).ToList()
                });
        }

        public async Task<RequestDto> GetRequestByIdAsync(int id, int userId)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found");
            }

            if (request.UserId != userId) // Kullanıcı sadece kendi taleplerini görebilir
            {
                throw new UnauthorizedAccessException("You are not authorized to view this request.");
            }

            return new RequestDto
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                actions = request.actions.Select(a => new ActionDto { ActionDescription = a.ActionDescription }).ToList()
            };
        }

        public async Task CreateRequestAsync(RequestDto requestDto)
        {
            var request = new Request
            {
                Title = requestDto.Title,
                Description = requestDto.Description,
                UserId = requestDto.UserId // DTO'dan gelen UserId kullanılıyor
            };

            await _unitOfWork.Requests.AddAsync(request);

            foreach (var actionDto in requestDto.actions)
            {
                var action = new Actions
                {
                    ActionDescription = actionDto.ActionDescription,
                    RequestId = request.Id
                };

                await _unitOfWork.Actions.AddAsync(action);
            }

            _unitOfWork.Commit(); // Commit all changes in one transaction
        }

        public async Task UpdateRequestAsync(int id, RequestDto requestDto)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found");
            }

            // Kullanıcı sadece kendi taleplerini güncelleyebilir
            if (request.UserId != requestDto.UserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this request.");
            }

            request.Title = requestDto.Title;
            request.Description = requestDto.Description;

            await _unitOfWork.Requests.UpdateAsync(request);

            // Güncellenen actions
            foreach (var actionDto in requestDto.actions)
            {
                var action = await _unitOfWork.Actions.GetByIdAsync(actionDto.Id);
                if (action != null)
                {
                    action.ActionDescription = actionDto.ActionDescription;
                    await _unitOfWork.Actions.UpdateAsync(action);
                }
            }

            _unitOfWork.Commit(); // Commit all changes in one transaction
        }

        public async Task DeleteRequestAsync(int id, int userId)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found");
            }

            // Kullanıcı sadece kendi taleplerini silebilir
            if (request.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this request.");
            }

            await _unitOfWork.Requests.DeleteAsync(id);

            foreach (var action in request.actions)
            {
                await _unitOfWork.Actions.DeleteAsync(action.Id);
            }

           _unitOfWork.Commit(); // Commit all changes in one transaction
        }
    }

}

