using System;
using System.Collections.Generic;
using System.Linq;
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

        User IUserRepository.FindById(Guid subjectId)
        {
            return _context.User.Find(subjectId).ToUser();
        }

        IEnumerable<User> IUserRepository.GetUsers()
        {
            return _context.User.Select(u => u.ToUser());
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
