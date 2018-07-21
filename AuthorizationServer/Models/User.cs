using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthorizationServer.Models
{
    public class User
    {
        public Guid SubjectId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
