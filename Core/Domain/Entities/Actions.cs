using System;
namespace Domain.Entities
{
	public class Actions
	{
		public int Id { get; set; }

		public string ActionDescription { get; set; }

		public int RequestId { get; set; }

		public Request Request { get; set; }

		
	}
}

