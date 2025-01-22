using System;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs
{
	public class ActionDto
	{
        public int Id { get; set; }
        [Required]
        public string ActionDescription { get; set; }

        
        public int RequestId { get; set; }

    }
}

