using System;
using System.Net;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Core.Entities;
namespace Persistence.Service
{

    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<RequestDto> GetRequestByIdAsync(int id)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {


                throw new Exception("Request not found"); 
            }

            // Entity'den DTO'ya dönüşüm
            return _mapper.Map<RequestDto>(request);

        
        }





        // Kullanıcı kimliğine göre requestleri getir
        public async Task<IEnumerable<RequestDto>> GetRequestsByUserIdAsync(int userId)
        {
            var requests = await _unitOfWork.Requests.GetAllAsync();

            // Kullanıcıya ait istekleri filtrele
            var userRequests = requests.Where(request => request.CreatedById == userId).ToList();

            // Entity listesinden DTO listesine dönüşüm
            return _mapper.Map<IEnumerable<RequestDto>>(userRequests);
        }






        public async Task<IEnumerable<RequestDto>> GetAllRequestsAsync()
        {
            var requests = await _unitOfWork.Requests.GetAllAsync();
            // Entity listesinden DTO listesine dönüşüm
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }





        public async Task AddRequestAsync(RequestDto dto)
        {
            // DTO'dan Entity'ye dönüşüm
            var request = _mapper.Map<Request>(dto);
            request.CreatedDate = DateTime.UtcNow; // Özel işlem yapılabilir

            await _unitOfWork.Requests.AddAsync(request);
            await _unitOfWork.SaveChangesAsync();
          
        }

        public async Task UpdateRequestAsync(int id, RequestDto dto)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {


                throw new Exception("Request not found");
            }

            // DTO'dan Entity'ye dönüşüm
            _mapper.Map(dto, request);

            _unitOfWork.Requests.Update(request);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRequestAsync(int id)
        {
            var request = await _unitOfWork.Requests.GetByIdAsync(id);
            if (request == null)
            {


                throw new Exception("Request not found");
            }
            _unitOfWork.Requests.Delete(request);
            await _unitOfWork.SaveChangesAsync();
        }
    }



}