using Application.DTOs;
using Application.Interfaces.Service;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Persistence.Service;

namespace Api.Controllers
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

        [HttpGet("ByRequest/{requestId}")]
        public async Task<IActionResult> GetActionsByRequestId(int requestId)
        {
            var actions = await _actionService.GetActionsByRequestIdAsync(requestId);
            return Ok(actions);
        }

        [HttpPost]
        public async Task<IActionResult> AddAction([FromBody] ActionDto dto)
        {
            try
            {


                await _actionService.AddActionAsync(dto);
                return Ok("Success");
            }

            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }

        }
    }
}

    

