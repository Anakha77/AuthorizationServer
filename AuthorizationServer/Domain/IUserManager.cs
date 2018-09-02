using AuthorizationServer.Dto;

namespace AuthorizationServer.Domain
{
    public interface IUserManager
    {
        User FindByUsername(string username);
    }
}