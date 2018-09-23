using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationServer.Dto;
using AuthorizationServer.Interfaces;
using IdentityModel;

namespace AuthorizationServer.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private List<User> _users;

        public InMemoryUserRepository()
        {
            _users = new List<User>
            {
                new User
                {
                    SubjectId = new Guid("5BE86359-073C-434B-AD2D-A3932222DABE"),
                    FirstName = "Test",
                    LastName = "Test",
                    Password = "password",
                    Username = "test@example.com"
                }
            };
        }

        public async Task AddAsync(User user)
        {
            user.SubjectId = Guid.NewGuid();
            await Task.Run(() => _users.Add(user));
            return;
        }

        public async Task<User> FindByIdAsync(Guid subjectId)
        {
            var user = _users.SingleOrDefault(u => u.SubjectId == subjectId);
            AddClaims(user);
            return await Task.FromResult(user);
        }

        private void AddClaims(User user)
        {
            user.Claims = new Claim[]
            {
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName),
                new Claim(JwtClaimTypes.Email, user.Username),
                new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
            };
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            var userToRemove = _users.SingleOrDefault(u => u.SubjectId == id);
            return Task.FromResult(userToRemove != null && _users.Remove(userToRemove));
        }

        public async Task<List<User>> ToListAsync()
        {
            return await Task.FromResult(_users);
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException("Update is not permitted for this type of repository");
        }
    }
}
