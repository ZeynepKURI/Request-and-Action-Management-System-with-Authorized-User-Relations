using Application.DTOs;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRequestsController(IRequestService requestService, IHttpContextAccessor httpContextAccessor)
        {
            _requestService = requestService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Get all requests for the logged-in user
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequestsByUserId()
        {
            try
            {
                // UserId'yi alıyoruz (token'dan veya başka bir kaynaktan)
                var userId = int.Parse(User.Identity.Name);

                // Servis katmanından kullanıcının taleplerini alıyoruz
                var requests = await _requestService.GetAllRequestsAsync(userId);

                // Talepler varsa, başarıyla döndürüyoruz
                return Ok(requests);
            }
            catch (Exception ex)
            {
                // Hata durumunda geri dönüyoruz
                return BadRequest(ex.Message);
            }
        }

        // Get a specific request by ID for the logged-in user
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<RequestDto>> GetRequestById(int id)
        {
            try
            {
                // UserId'yi alıyoruz
                var userId = int.Parse(User.Identity.Name);

                // Servis katmanından belirtilen ID'ye ait talebi alıyoruz
                var request = await _requestService.GetRequestByIdAsync(id, userId);

                // Talep bulunursa, başarıyla döndürüyoruz
                return Ok(request);
            }
            catch (Exception ex)
            {
                // Hata durumunda geri dönüyoruz
                return BadRequest(ex.Message);
            }
        }

        // Create a new request for the logged-in user
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> CreateRequest([FromBody] RequestDto requestDto)
        {
            try
            {
                // UserId'yi alıyoruz ve requestDto'ya ekliyoruz
                var userId = int.Parse(User.Identity.Name);
                requestDto.UserId = userId;

                // Servis katmanına yeni talebi oluşturma isteği gönderiyoruz
                await _requestService.CreateRequestAsync(requestDto);

                // Başarılı ise, cevap döndürüyoruz
                return Ok("Request created successfully");
            }
            catch (Exception ex)
            {
                // Hata durumunda geri dönüyoruz
                return BadRequest(ex.Message);
            }
        }

        // Update an existing request for the logged-in user
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> UpdateRequest(int id, [FromBody] RequestDto requestDto)
        {
            try
            {
                // UserId'yi alıyoruz ve requestDto'ya ekliyoruz
                var userId = int.Parse(User.Identity.Name);
                requestDto.UserId = userId;

                // Servis katmanına talebi güncelleme isteği gönderiyoruz
                await _requestService.UpdateRequestAsync(id, requestDto);

                // Başarılı ise, cevap döndürüyoruz
                return Ok("Request updated successfully");
            }
            catch (Exception ex)
            {
                // Hata durumunda geri dönüyoruz
                return BadRequest(ex.Message);
            }
        }

        // Delete a request for the logged-in user
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> DeleteRequest(int id)
        {
            try
            {
                // UserId'yi alıyoruz
                var userId = int.Parse(User.Identity.Name);

                // Servis katmanına talebi silme isteği gönderiyoruz
                await _requestService.DeleteRequestAsync(id, userId);

                // Başarılı ise, cevap döndürüyoruz
                return Ok("Request deleted successfully");
            }
            catch (Exception ex)
            {
                // Hata durumunda geri dönüyoruz
                return BadRequest(ex.Message);
            }
        }
    }
}
