using Application.DTOs;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionService _actionService;

        public ActionController(IActionService actionService)
        {
            _actionService = actionService;
        }

        // Kullanıcıların aksiyonları görmesi sağlanır.
        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetAllActions()
        {
            var actions = await _actionService.GetAllActionsAsync();
            return Ok(actions);
        }

        // Aksiyon ID'ye göre alınabilir.
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetActionById(int id)
        {
            try
            {
                var action = await _actionService.GetActionByIdAsync(id);
                return Ok(action);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Adminler aksiyon oluşturabilir, atanan kullanıcıyı belirleyebilir
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateAction([FromBody] ActionDto actionDto)
        {
            try
            {
                await _actionService.CreateActionAsync(actionDto);
                return CreatedAtAction(nameof(GetActionById), new { id = actionDto.Id }, actionDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // Adminler aksiyon güncelleyebilir
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateAction(int id, [FromBody] ActionDto actionDto)
        {
            try
            {
                await _actionService.UpdateActionAsync(id, actionDto);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // Adminler aksiyon silebilir
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAction(int id)
        {
            try
            {
                await _actionService.DeleteActionAsync(id);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
