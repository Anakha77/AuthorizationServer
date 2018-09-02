using AuthorizationServer.Interfaces;
using AuthorizationServer.Models;
using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthorizationServer.Data
{
    public class Users : IUserRepository
    {
        private List<User> _users;

        public Users()
        {
            _users = new List<User>()
            {
                new User
                {
                    SubjectId = Guid.NewGuid(),
                    Username = "csorlin",
                    Password = "password",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Email, "csorlin@gmail.com"),
                        new Claim(JwtClaimTypes.Role, "user")
                }                }
            };
        }

        public User FindById(Guid subjectId)
        {
            return _users.FirstOrDefault(u => u.SubjectId == subjectId);
        }
        public User FindByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }
}
