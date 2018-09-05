using System;
using System.Collections.Generic;
using System.Security.Claims;
using AuthorizationServer.Dto;
using AuthorizationServer.Interfaces;
using IdentityModel;

namespace AuthorizationServer.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private IList<User> _users;

        public InMemoryUserRepository()
        {
            _users = new List<User>();
            _users.Add(new User
            {
                SubjectId = new Guid("5BE86359-073C-434B-AD2D-A3932222DABE"),
                FirstName = "Test",
                LastName = "Test",
                Password = "password",
                Username = "test@example.com",
                Claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Mr. Test"),
                    new Claim(JwtClaimTypes.Role, "user")
                }
            });
        }

        public User FindById(Guid subjectId)
        {
            return _users[0];
        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }
}
