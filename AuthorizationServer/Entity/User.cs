using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AuthorizationServer.Entity
{
    public class User
    {
        [Key]
        public Guid SubjectId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public List<Claim> Claims { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
