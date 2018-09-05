using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<User> FindByIdAsync(string id)
        {
            return await _userRepository.FindByIdAsync(new Guid(id));
        }

        public User FindByUsername(string username)
        {
            return _userRepository.GetUsers().FirstOrDefault(u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
