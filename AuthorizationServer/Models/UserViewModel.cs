using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthorizationServer.Models
{
    public class UserViewModel
    {
        public UserViewModel(Dto.User user)
        {
            SubjectId = user.SubjectId;
            Username = user.Username;
            Password = user.Password;
            Claims = user.Claims?.ToList();
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
        public Guid SubjectId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Claim> Claims { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
