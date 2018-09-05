using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
                Username = "test",
                Claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Email, "test@example.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                }
            });
        }

        Task<User> IUserRepository.FindByIdAsync(Guid subjectId)
        {
            return Task.FromResult(_users[0]);
        }

        IEnumerable<User> IUserRepository.GetUsers()
        {
            return _users;
        }
    }
}
