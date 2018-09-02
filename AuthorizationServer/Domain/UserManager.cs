using System;
using System.Linq;
using AuthorizationServer.Dto;
using AuthorizationServer.Interfaces;

namespace AuthorizationServer.Domain
{
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User FindByUsername(string username)
        {
            return _userRepository.GetUsers().First(u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
