using System;
using Application.DTOs;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using Core.Entities;

namespace Persistence.Service
{
    public class ActionService : IActionService
    {

        // ActionService, IActionService interface'ini uygulayan bir servis sınıfıdır.
        private readonly IUnitOfWork _unitOfWork;


        // Constructor: Unit of Work bağımlılığı enjeksiyon yoluyla alınır.
        public ActionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; // Gelen Unit of Work instance'ı sınıf değişkenine atanır.
        }


        // Bir isteğe ait tüm aksiyonları döndürür.
        public async Task<IEnumerable<ActionDto>> GetActionsByRequestIdAsync(int requestId)
        {
            var actions = await _unitOfWork.Actions.GetAllAsync();
            return actions
                .Where(action => action.RequestId == requestId)
                .Select(action => new ActionDto
                {
                    Id = action.Id,
                    Description = action.Description,
                    RequestId = action.RequestId,
                    AssignedToId = action.AssignedToId,
                    AssignedToName = action.AssignedTo?.Name,
                    Status = action.Status,
                    // Burada UTC zamanını atıyoruz
                    CreatedDate = action.CreatedDate.ToUniversalTime(),  // UTC zamanını ekliyoruz
                    Deadline = action.Deadline.ToUniversalTime() // Eğer deadline yerel bir saat ise, UTC'ye dönüştür

                    
                }).ToList();
        }


        // Yeni bir aksiyon ekleyen metot.
        public async Task AddActionAsync(ActionDto dto)
        {
            var action = new Actions
            {
                Description = dto.Description,
                RequestId = dto.RequestId,
                AssignedToId = 1, // Örnek bir kullanıcı ID
                                  // Tarihleri UTC'ye dönüştür
                CreatedDate = dto.CreatedDate.ToUniversalTime(),  // UTC'ye dönüştür
                Deadline = dto.Deadline.ToUniversalTime() ,        // UTC'ye dönüştür


                Status = dto.Status
            };

            await _unitOfWork.Actions.AddAsync(action);   // Aksiyonu veritabanına ekler.
            await _unitOfWork.SaveChangesAsync();    // Değişiklikleri kaydeder.
        }
    }

}

