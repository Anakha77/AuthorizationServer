using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorizationServer.Dto;

namespace AuthorizationServer.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>>ToListAsync();

        Task<User> FindByIdAsync(Guid subjectId);

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<bool> RemoveAsync(Guid id);
    }
}
