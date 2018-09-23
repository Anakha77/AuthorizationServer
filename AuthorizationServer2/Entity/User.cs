using System;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Entity
{
    public class User
    {
        [Key]
        public Guid SubjectId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
