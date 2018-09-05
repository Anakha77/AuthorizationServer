using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace AuthorizationServer.Domain
{
    public class ProfileService : IProfileService
    {
        private readonly IUserManager _userManager;

        public ProfileService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No sub claim present");

            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                return;
            }
            else
            {
                var identity = new ClaimsIdentity();
                identity.AddClaims(user.Claims);
                var principal = new ClaimsPrincipal(identity);


                context.AddRequestedClaims(principal.Claims);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject?.GetSubjectId();
            if (sub == null)
            {
                throw new Exception("No subject Id claim present");
            }

            var user = await _userManager.FindByIdAsync(sub);

            context.IsActive = user != null;
        }
    }
}
