using Core.Entities.Core.Entities;

namespace Core.Entities
{
    public class Actions
    {
        public int Id { get; set; } // Aksiyon ID'si
        
        public DateTime CreatedAt { get; set; } // Aksiyonun oluşturulma tarihi
        public string Status { get; set; } // Aksiyonun durumu (başlangıçta 'Started' olabilir)

        // AssignedTo => Aksiyonun atanacağı kullanıcı
        public int AssignedTo { get; set; }
        public User AssignedUser { get; set; } // AssignedUser, kullanıcı ile ilişkiyi belirtir

        // RequestId => Aksiyonun ait olduğu talep
        public int RequestId { get; set; }
        public Request Request { get; set; } // Aksiyonun ait olduğu talep
    }
}
