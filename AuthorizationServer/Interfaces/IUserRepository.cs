using AuthorizationServer.Models;
using System;
using System.Collections.Generic;

namespace AuthorizationServer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User FindById(Guid subjectId);
        User FindByUsername(string username);
    }
}
