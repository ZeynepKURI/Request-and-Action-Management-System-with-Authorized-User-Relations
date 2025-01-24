
namespace Application.DTOs
{
    public class RequestDto
    {
        public int Id { get; set; }
        public int CreatedById { get; set; } // Talebi oluşturan kullanıcının ID'si
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
