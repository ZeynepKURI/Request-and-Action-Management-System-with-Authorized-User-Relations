using System;
using Application.DTOs;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using Core.Entities;

namespace Persistence.Service
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ActionDto>> GetActionsByRequestIdAsync(int requestId)
        {
            var actions = await _unitOfWork.Actions.GetAllAsync();
            return actions
                .Where(action => action.RequestId == requestId)
                .Select(action => new ActionDto
                {
                    Id = action.Id,
                    Description = action.Description,
                    RequestId = action.RequestId,
                    AssignedToId = action.AssignedToId,
                    AssignedToName = action.AssignedTo?.Name,
                    Status = action.Status,
                    // Burada UTC zamanını atıyoruz
                    CreatedDate = action.CreatedDate.ToUniversalTime(),  // UTC zamanını ekliyoruz
                    Deadline = action.Deadline.ToUniversalTime() // Eğer deadline yerel bir saat ise, UTC'ye dönüştür

                    
                }).ToList();
        }

        public async Task AddActionAsync(ActionDto dto)
        {
            var action = new Actions
            {
                Description = dto.Description,
                RequestId = dto.RequestId,
                AssignedToId = 1, // Örnek bir kullanıcı ID
                                  // Tarihleri UTC'ye dönüştür
                CreatedDate = dto.CreatedDate.ToUniversalTime(),  // UTC'ye dönüştür
                Deadline = dto.Deadline.ToUniversalTime() ,        // UTC'ye dönüştür


                Status = dto.Status
            };

            await _unitOfWork.Actions.AddAsync(action);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}

