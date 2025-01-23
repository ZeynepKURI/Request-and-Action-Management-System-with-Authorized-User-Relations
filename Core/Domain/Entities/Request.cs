
using Core.Entities.Core.Entities;

namespace Core.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }  // Foreign Key

        // İlişkiler
        public User User { get; set; } // Kullanıcıya ait
        public ICollection<Actions> Actions { get; set; }  // Bu talep üzerindeki aksiyonlar
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } // "Pending", "InProgress", "Completed"
    }
}

//Request ve Action arasındaki ilişkiyi bir One-to-Many ilişkisi olarak tanımlıyoruz.