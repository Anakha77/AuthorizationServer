using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Data;
using AuthorizationServer.Dto;
using AuthorizationServer.Interfaces;

namespace AuthorizationServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ServerDbContext _context;

        public UserRepository(ServerDbContext context)
        {
            _context = context;
        }

        IEnumerable<User> IUserRepository.GetUsers()
        {
            return _context.User.Select(u => u.ToUser());
        }

        async Task<User> IUserRepository.FindByIdAsync(Guid subjectId)
        {
            var user = await _context.User.FindAsync(subjectId);

            return user.ToUser();
        }
    }

    public static class Extentions
    {
        public static User ToUser(this Entity.User user)
        {
            return new User
            {
                SubjectId = user.SubjectId,
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
