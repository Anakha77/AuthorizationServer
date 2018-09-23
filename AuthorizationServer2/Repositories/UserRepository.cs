using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Data;
using AuthorizationServer.Dto;
using AuthorizationServer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ServerDbContext _context;

        public UserRepository(ServerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.User.AddAsync(user.ToEntity());
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<User> FindByIdAsync(Guid subjectId)
        {
            var user = await _context.User.FindAsync(subjectId);
            return user.ToUser();
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            try
            {
                var userToRemove = await _context.User.SingleOrDefaultAsync(u => u.SubjectId == id);
                _context.Remove(userToRemove);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<List<User>> ToListAsync()
        {
            var users = await _context.User.ToListAsync();
            return users.Select(u => u.ToUser()).ToList();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user.ToEntity());
            await _context.SaveChangesAsync();
            return;
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
        public static Entity.User ToEntity(this User user)
        {
            return new Entity.User
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
