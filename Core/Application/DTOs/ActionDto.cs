namespace Application.DTOs
{
    public class ActionDto
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int AssignedTo { get; set; }
        public string AssignedUserName { get; set; }
        
        public DateTime ActionDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
