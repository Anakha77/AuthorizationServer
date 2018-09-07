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
                    Username = "test",
                    Claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Email, "test@example.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                }
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
            return await Task.FromResult(_users[0]);
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            var userToRemove = _users.SingleOrDefault(u => u.SubjectId == id);
            return Task.FromResult(userToRemove!=null && _users.Remove(userToRemove));
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
