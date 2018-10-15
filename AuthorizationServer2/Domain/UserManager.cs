using System;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Dto;
using AuthorizationServer.Interfaces;

namespace AuthorizationServer.Repositories
{
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task<User> FindByIdAsync(string id)
        {
            return await _userRepository.FindByIdAsync(new Guid(id));
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            var users = await _userRepository.ToListAsync();
            return users.SingleOrDefault(u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
