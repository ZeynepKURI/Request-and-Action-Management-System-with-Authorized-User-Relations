using Application.DTOs;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Persistence.Services
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ActionService> _logger;


        public ActionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ActionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ActionDto>> GetAllActionsAsync()
        {
            var actions = await _unitOfWork.Actions.GetAllAsync();
            return _mapper.Map<IEnumerable<ActionDto>>(actions);
        }

            public async Task<ActionDto> GetActionByIdAsync(int id)
        {
            var action = await _unitOfWork.Actions.GetByIdAsync(id);
            if (action == null)
            {
                _logger.LogWarning($"Action with ID {id} not found.");
                throw new Exception("Action not found");
            }

            return _mapper.Map<ActionDto>(action);
        }

        public async Task CreateActionAsync(ActionDto actionDto)
        {
            var action = _mapper.Map<Actions>(actionDto);
            await _unitOfWork.Actions.AddAsync(action);
             _unitOfWork.Commit();
        }
    

        public async Task UpdateActionAsync(int id, ActionDto actionDto)
        {
            var action = await _unitOfWork.Actions.GetByIdAsync(id);
            if (action == null)
            {
                throw new Exception("Action not found");
            }

            _mapper.Map(actionDto, action);
            await _unitOfWork.Actions.UpdateAsync(action);
         _unitOfWork.Commit();
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
