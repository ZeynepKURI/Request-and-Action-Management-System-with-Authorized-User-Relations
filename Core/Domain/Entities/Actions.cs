

namespace Core.Entities
{
    public class Actions
    {
        public int Id { get; set; }
        public string Description { get; set; } // Aksiyon açıklaması
        public int RequestId { get; set; } // Talep ID'si
        public Request Request { get; set; } // Talep ilişkisi
        public int AssignedToId { get; set; } // Aksiyonun atandığı kullanıcının ID'si
        public User AssignedTo { get; set; } // Aksiyonun atandığı kullanıcı
        public DateTime CreatedDate { get; set; } // Aksiyon oluşturulma tarihi
        public DateTime Deadline { get; set; } // Aksiyon için son tarih
        public string Status { get; set; } // Aksiyon durumu
    }

}

