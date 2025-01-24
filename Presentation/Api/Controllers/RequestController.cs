using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Service;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    // RequestController: Kullanıcı isteklerini yönetmek için API endpointlerini içerir.

    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;


        // Dependency Injection (Bağımlılık enjeksiyonu) ile istek servisi alınır.
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }



        [HttpGet]      // Tüm istekleri getiren endpoint.
        [Authorize(Policy = "Admin")]   // Sadece Admin yetkisi olan kullanıcılar erişebilir.
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var requests = await _requestService.GetAllRequestsAsync(); // Tüm istekler servis üzerinden alınır.
                return Ok(requests); // Başarılı olursa istekler döndürülür.
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // ID ile tek bir isteği getiren endpoint
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]   // Sadece Admin yetkisi olan kullanıcılar erişebilir.
        public async Task<IActionResult> GetRequestById(int id)
        {
            try
            {
                var request = await _requestService.GetRequestByIdAsync(id);  // Belirtilen ID'ye göre istek alınır.
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






        // Yeni bir istek ekleyen endpoint.
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


        // Bir isteği güncelleyen endpoint.
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


        // Bir isteği silen endpoint.
        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> DeleteRequest(int id)   // Servis üzerinden istek silinir.
        {
            try
            {
                await _requestService.DeleteRequestAsync(id);
                return NoContent();  // Başarılı olursa boş içerik döndürülür (204 No Content).
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
      
        }
    }



}