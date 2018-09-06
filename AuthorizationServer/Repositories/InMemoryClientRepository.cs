using System.Threading.Tasks;
using AuthorizationServer.Interfaces;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizationServer.Repositories
{
    public class InMemoryClientRepository : IClientRepository
    {
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var mvc = new Client
            {
                ClientId = "client.mvc",
                ClientName = "Client MVC",
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                RequireConsent = true,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RedirectUris = { "http://localhost:5002/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email
                },
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true
            };

            return Task.FromResult(mvc);
        }
    }
}
