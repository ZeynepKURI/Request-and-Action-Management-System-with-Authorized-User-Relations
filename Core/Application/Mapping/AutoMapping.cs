using System;
using Application.DTOs;
using AutoMapper;
using Core.Entities;


namespace Application.Mapping
{
	public class AutoMapping : Profile
	{
        // Entity -> DTO dönüşümü
        public AutoMapping()
		{
			CreateMap<Actions, ActionDto>().ReverseMap();// TAM TERSİNE DE DÖNÜŞTÜRÜR.
			CreateMap<Request, RequestDto>().ReverseMap();

		}

	}
}

