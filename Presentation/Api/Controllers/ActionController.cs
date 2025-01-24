using Application.DTOs;
using Application.Interfaces.Service;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
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


        // Belirli bir Request ID'ye göre action bilgilerini getiren endpoint.

        [HttpGet("ByRequest/{requestId}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetActionsByRequestId(int requestId)
        {
            try
            {    // Servis katmanından belirtilen Request ID'ye göre verileri alır.
                var actions = await _actionService.GetActionsByRequestIdAsync(requestId);
                return Ok(actions);          // Başarılı bir şekilde alınırsa 200 OK ve veriler döndürülür.

            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // Yeni bir action eklemek için kullanılan endpoint.
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAction([FromBody] ActionDto dto)  // Body'den JSON olarak gelen ActionDto'yu alır.
        {
            {
                try
                {
                    // Servis katmanı kullanılarak yeni bir action eklenir.

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
}
        

    

