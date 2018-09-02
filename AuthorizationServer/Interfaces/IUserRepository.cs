using System;
using System.Collections.Generic;
using AuthorizationServer.Dto;

namespace AuthorizationServer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User FindById(Guid subjectId);
    }
}
