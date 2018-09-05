using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorizationServer.Dto;

namespace AuthorizationServer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        Task<User> FindByIdAsync(Guid subjectId);
    }
}
