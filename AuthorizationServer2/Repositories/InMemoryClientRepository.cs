using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Interfaces;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizationServer.Repositories
{
    public class InMemoryClientRepository : IClientRepository
    {
        private readonly List<Client> _clients;

        public InMemoryClientRepository()
        {
            _clients = new List<Client>
            {
                new Client
                {
                    ClientId = "client.mvc",
                    ClientName = "Client MVC",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    RequireConsent = false,
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
                },
                new Client
                {
                    ClientId = "client.ed.guardians",
                    ClientName = "ED Guardians",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = {
                        "http://localhost:4200/",
                        "https://localhost:50001/",
                        "http://localhost:5001/"
                    },
                    PostLogoutRedirectUris = {
                        "http://localhost:4200/",
                        "https://localhost:50001/",
                        "http://localhost:5001/"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "ed.guardians.api"
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedCorsOrigins =
                    {
                        "http://localhost:4200",
                        "https://localhost:50001",
                        "http://localhost:5001"
                    }
                }
            };
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var mvc = _clients.SingleOrDefault(c => c.ClientId.Equals(clientId));

            return Task.FromResult(mvc);
        }
    }
}
