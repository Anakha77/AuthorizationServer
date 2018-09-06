using System.Threading.Tasks;
using IdentityServer4.Models;

namespace AuthorizationServer.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> FindClientByIdAsync(string clientId);
    }
}
