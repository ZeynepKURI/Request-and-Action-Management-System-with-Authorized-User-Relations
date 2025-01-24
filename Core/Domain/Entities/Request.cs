

namespace Core.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public string Title { get; set; } // Talep başlığı
        public string Description { get; set; } // Talep açıklaması
        public string Status { get; set; } // Talep durumu (e.g., Open, Closed)
        public int CreatedById { get; set; } // Talebi oluşturan kullanıcının ID'si
        public User CreatedBy { get; set; } // Talebi oluşturan kullanıcı
        public DateTime CreatedDate { get; set; } // Talep oluşturulma tarihi
        public ICollection<Actions> Actions { get; set; } // Aksiyonlarla ilişki
    }

}



//Request ve Action arasındaki ilişkiyi bir One-to-Many ilişkisi olarak tanımlıyoruz.