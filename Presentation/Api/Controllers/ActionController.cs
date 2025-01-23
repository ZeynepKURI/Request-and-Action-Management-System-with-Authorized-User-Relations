using Application.DTOs;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionController : ControllerBase
    {
        private readonly IActionService _actionService;

        public ActionController(IActionService actionService)
        {
            _actionService = actionService;
        }

        // Tüm aksiyonları getiren GET metodu
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllActions()
        {
            try
            {
                var actions = await _actionService.GetAllActionsAsync();
                return Ok(actions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ID'ye göre bir aksiyon getiren GET metodu
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetActionById(int id)
        {
            try
            {
                var action = await _actionService.GetActionByIdAsync(id);
                if (action == null)
                {
                    return NotFound($"Action with id {id} not found");
                }
                return Ok(action);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Yeni bir aksiyon oluşturmak için kullanılan POST metodu
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAction([FromBody] ActionDto actionDto)
        {
            if (actionDto == null)
            {
                return BadRequest("Action data is required");
            }

            try
            {
                await _actionService.CreateActionAsync(actionDto);
                return CreatedAtAction(nameof(GetActionById), new { id = actionDto.Id }, actionDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Var olan bir aksiyonu güncellemek için kullanılan PUT metodu
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAction(int id, [FromBody] ActionDto actionDto)
        {
            if (actionDto == null)
            {
                return BadRequest("Action data is required");
            }

            try
            {
                await _actionService.UpdateActionAsync(id, actionDto);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Var olan bir aksiyonu silmek için kullanılan DELETE metodu
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAction(int id)
        {
            try
            {
                await _actionService.DeleteActionAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
