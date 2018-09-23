using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthorizationServer
{
    public class Config
    {
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource {
                    Name = "customAPI",
                    DisplayName = "Custom API",
                    Description = "Custom API Access",
                    UserClaims = new List<string> {"role"},
                    ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                    Scopes = new List<Scope> {
                        new Scope("customAPI.read"),
                        new Scope("customAPI.write")
                    }
                },
                new ApiResource
                {
                    Name = "ed.guardians.api",
                    DisplayName = "ED Guardians API",
                    Scopes = new List<Scope>
                    {
                        new Scope("ed.guardians.api")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var emailResource = new IdentityResource("email", "Email", new[] { "email", "email_verified" });
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                emailResource
            };
        }
    }
}
