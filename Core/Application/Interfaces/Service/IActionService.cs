using System;
using Application.DTOs;

namespace Application.Interfaces.Service
{
	
        public interface IActionService
        {
            Task<IEnumerable<ActionDto>> GetActionsByRequestIdAsync(int requestId);
            Task AddActionAsync(ActionDto dto);
        }

    
}

