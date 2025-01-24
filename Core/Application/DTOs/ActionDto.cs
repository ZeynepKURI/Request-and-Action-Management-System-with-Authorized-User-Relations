namespace Application.DTOs
{
    public class ActionDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        // Action ile ilişkili Request'in kimliği
        public int RequestId { get; set; }

            // Action'ı atanan kullanıcının kimliği
        public int AssignedToId { get; set; }

        // Action'ı atanan kullanıcının adı
        public string AssignedToName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
    }

}


