using System;
using Application.DTOs;

namespace Application.Interfaces.Service
{
	public interface IActionService
	{
		Task<IEnumerable<ActionDto>> GetAllActionAsync();

		Task<ActionDto> GetActionByIdAsync(int id);

		Task AddActionAsync(ActionDto actionDto);

		Task UpdateActionAsync(int id, ActionDto actionDto);

		Task DeleteActionAsync(int id);






	}
}

