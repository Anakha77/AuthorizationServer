using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers
{
    public class ConsentController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public ConsentController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var grantedConsent = new ConsentResponse
            {
                RememberConsent = true,
                ScopesConsented = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            };

            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
            {
                return View("Error");
            }

            // communicate outcome of consent back to identityserver
            await _interaction.GrantConsentAsync(request, grantedConsent);

            return Redirect(returnUrl);
        }
    }
}