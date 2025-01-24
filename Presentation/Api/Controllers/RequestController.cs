using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Service;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Service;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }



        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var requests = await _requestService.GetAllRequestsAsync();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            try
            {
                var request = await _requestService.GetRequestByIdAsync(id);
                if (request == null) return NotFound();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest (ex.Message);
            }
        
        }


        // Kullanıcı kimliği ile istekleri getir
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRequestsByUserId(int userId)
        {
            var requests = await _requestService.GetRequestsByUserIdAsync(userId);
            if (requests == null || !requests.Any())
            {
                return NotFound("No requests found for this user.");
            }

            return Ok(requests);
        }







        [HttpPost]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> AddRequest([FromBody] RequestDto dto)
        {
            try
            {
                await _requestService.AddRequestAsync(dto);
                return Ok("Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] RequestDto dto)
        {
            try
            {
                await _requestService.UpdateRequestAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            try
            {
                await _requestService.DeleteRequestAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
      
        }
    }



}