using System;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs
{
	public class ActionDto
	{

        [Required]
        public string ActionDescription { get; set; }

        
        public int RequestId { get; set; }

    }
}

