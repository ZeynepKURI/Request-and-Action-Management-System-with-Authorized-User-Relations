using Application.DTOs;

namespace Application.Interfaces.Service
{
    public interface IActionService
    {
        Task<IEnumerable<ActionDto>> GetAllActionsAsync();
        Task<ActionDto> GetActionByIdAsync(int id);
        Task CreateActionAsync(ActionDto actionDto);
        Task UpdateActionAsync(int id, ActionDto actionDto);
        Task DeleteActionAsync(int id);
    }
}
