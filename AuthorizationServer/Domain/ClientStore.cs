using System.Threading.Tasks;
using AuthorizationServer.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthorizationServer.Repositories
{
    public class ClientStore : IClientStore
    {
        private readonly IClientRepository _clientManager;

        public ClientStore(IClientRepository clientManager)
        {
            _clientManager = clientManager;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            return await _clientManager.FindClientByIdAsync(clientId);
        }
    }
}
