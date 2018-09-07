using System.Threading.Tasks;
using AuthorizationServer.Dto;

namespace AuthorizationServer.Repositories
{
    public interface IUserManager
    {
        Task<User> FindByUsernameAsync(string username);

        Task<User> FindByIdAsync(string id);
    }
}