using Application.DTOs;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // Tüm talepleri almak için
        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }

        // ID'ye göre tek bir talebi almak için
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            try
            {
                var request = await _requestService.GetRequestByIdAsync(id);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Yeni bir talep oluşturmak için
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateRequest([FromBody] RequestDto requestDto)
        {
            try
            {
                await _requestService.CreateRequestAsync(requestDto);
                return CreatedAtAction(nameof(GetRequestById), new { id = requestDto.Id }, requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Bir talebi güncellemek için
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] RequestDto requestDto)
        {
            try
            {
                await _requestService.UpdateRequestAsync(id, requestDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Bir talebi silmek için
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            try
            {
                await _requestService.DeleteRequestAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Talep statüsünü güncellemek için
        [HttpPut("{id}/status")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateRequestStatus(int id, [FromBody] string status)
        {
            try
            {
                await _requestService.UpdateRequestStatusAsync(id, status);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
