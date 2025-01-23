namespace Application.DTOs
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }  // Foreign Key
        public string Status { get; set; } // "Pending", "InProgress", "Completed"
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
