﻿namespace Core.Entities
{
        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Role { get; set; } = "User";  // "User" ya da "Admin"
            public string? Password { get; set; } = "";

            // İlişkiler
            public ICollection<Request>? CreatedRequests { get; set; } // Kullanıcının talepleri
            public ICollection<Actions>? AssignedActions { get; set; } // Admin ise oluşturduğu aksiyonlar
        }
    
}