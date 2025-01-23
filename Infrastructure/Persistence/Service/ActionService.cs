using Application.DTOs;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Persistence.Services
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ActionService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ActionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ActionService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        // Kullanıcının kimliğini almak için yardımcı metot
        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : 0; // Eğer kullanıcı ID'si mevcut değilse 0 döner
        }

        public async Task<IEnumerable<ActionDto>> GetAllActionsAsync()
        {
            var actions = await _unitOfWork.Actions.GetAllAsync();
            return _mapper.Map<IEnumerable<ActionDto>>(actions);
        }

        public async Task<ActionDto> GetActionByIdAsync(int id)
        {
            var action = await _unitOfWork.Actions.GetByIdAsync(id);
            if (action == null)
            {
                _logger.LogWarning($"Aksiyon ID {id} ile bulunamadı.");
                throw new Exception("Aksiyon bulunamadı.");
            }

            return _mapper.Map<ActionDto>(action);
        }

        public async Task CreateActionAsync(ActionDto actionDto)
        {
            var action = _mapper.Map<Actions>(actionDto);

            // Şu anda oturum açan kullanıcıya aksiyon ataması yapılıyor
            action.AssignedTo = GetCurrentUserId();  // Aksiyonu mevcut kullanıcıya ata
            action.CreatedAt = DateTime.UtcNow;
            action.Status = "Started";  // Başlangıç durumu olarak atama yapılır.

            await _unitOfWork.Actions.AddAsync(action);
            _unitOfWork.Commit();
        }

        public async Task UpdateActionAsync(int id, ActionDto actionDto)
        {
            var action = await _unitOfWork.Actions.GetByIdAsync(id);
            if (action == null)
            {
                throw new Exception("Aksiyon bulunamadı.");
            }

            _mapper.Map(actionDto, action);
            action.AssignedTo = GetCurrentUserId();  // Aksiyonu mevcut kullanıcıya atama (eğer gerekiyorsa)
            action.Status = "Updated";  // Durum güncellenebilir

            await _unitOfWork.Actions.UpdateAsync(action);
            _unitOfWork.Commit();
        }

        public async Task DeleteActionAsync(int id)
        {
            var action = await _unitOfWork.Actions.GetByIdAsync(id);
            if (action == null)
            {
                throw new Exception("Aksiyon bulunamadı.");
            }

            await _unitOfWork.Actions.DeleteAsync(id);
            _unitOfWork.Commit(); // Tüm değişiklikleri bir işlemde kaydet
        }
    }
}

