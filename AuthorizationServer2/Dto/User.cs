using System;
using System.Security.Claims;

namespace AuthorizationServer.Dto
{
    public class User
    {
        public Guid SubjectId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Claim[] Claims { get; set; }
    }
}
