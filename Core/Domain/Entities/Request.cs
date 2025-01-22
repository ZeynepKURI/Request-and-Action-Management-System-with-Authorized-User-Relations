using System;
namespace Domain.Entities
{
	public class Request
	{
	      public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public int UserId { get; set; }

		public User User { get; set; }

		public ICollection<Actions> actions { get; set; }
	}
}

//Request ve Action arasındaki ilişkiyi bir One-to-Many ilişkisi olarak tanımlıyoruz.