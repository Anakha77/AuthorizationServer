using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;

namespace AuthorizationServer.Domain
{
    public class CorsPolicyService : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            string[] allowedOrigins =
            {
                "http://localhost:4200",
                "https://localhost:50001",
                "http://localhost:5001",
                "http://localhost:5002"
            };
            return Task.FromResult(allowedOrigins.Contains(origin));
        }
    }
}
