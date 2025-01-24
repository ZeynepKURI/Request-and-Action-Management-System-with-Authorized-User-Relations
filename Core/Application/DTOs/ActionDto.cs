namespace Application.DTOs
{
    public class ActionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int RequestId { get; set; }
        public int AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
    }

}


