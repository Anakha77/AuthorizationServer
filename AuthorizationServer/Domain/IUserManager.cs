using System.Threading.Tasks;
using AuthorizationServer.Dto;

namespace AuthorizationServer.Repositories
{
    public interface IUserManager
    {
        User FindByUsername(string username);

        Task<User> FindByIdAsync(string id);
    }
}