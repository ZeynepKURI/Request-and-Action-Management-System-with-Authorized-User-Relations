namespace Core.Entities
{
    namespace Core.Entities
    {
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string? Email { get; set; }
            public string? Role { get; set; } = "User";  // "User" ya da "Admin"
            public string? Password { get; set; } = "";

            // Kullanıcıya ait aksiyonlar
            public ICollection<Actions> AssignedActions { get; set; }
            public ICollection<Request> AssignedRequests { get; set; } // Kullanıcının atadığı talepler
        }
    }
}