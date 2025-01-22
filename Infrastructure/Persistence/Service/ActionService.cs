using Application.DTOs;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;

namespace Persistence.Services
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ActionDto>> GetAllActionsAsync()
        {
            var actions = await _unitOfWork.Actions.GetAllAsync();
            return actions.Select(a => new ActionDto
            {
                Id = a.Id,
                ActionDescription = a.ActionDescription,
                RequestId = a.RequestId
            });
        }

        public async Task<ActionDto> GetActionByIdAsync(int id)
        {
            var action = await _unitOfWork.Actions.GetByIdAsync(id);
            if (action == null)
            {
                throw new Exception("Action not found");
            }

            return new ActionDto
            {
                Id = action.Id,
                ActionDescription = action.ActionDescription,
                RequestId = action.RequestId
            };
        }

        public async Task CreateActionAsync(ActionDto actionDto)
        {
            var action = new Actions
            {
                ActionDescription = actionDto.ActionDescription,
                RequestId = actionDto.RequestId  // Bu Action'ın hangi Request'e ait olduğunu belirtir
            };

            await _unitOfWork.Actions.AddAsync(action);
            _unitOfWork.Commit(); // Commit all changes in one transaction
        }

        public async Task UpdateActionAsync(int id, ActionDto actionDto)
        {
            var action = await _unitOfWork.Actions.GetByIdAsync(id);
            if (action == null)
            {
                throw new Exception("Action not found");
            }

            action.ActionDescription = actionDto.ActionDescription;
            action.RequestId = actionDto.RequestId;

            await _unitOfWork.Actions.UpdateAsync(action);
           _unitOfWork.Commit(); // Commit all changes in one transaction
        }

        public async Task DeleteActionAsync(int id)
        {
            var action = await _unitOfWork.Actions.GetByIdAsync(id);
            if (action == null)
            {
                throw new Exception("Action not found");
            }

            await _unitOfWork.Actions.DeleteAsync(id);
            _unitOfWork.Commit(); // Commit all changes in one transaction
        }
    }
}
