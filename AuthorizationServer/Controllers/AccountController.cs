﻿using AuthorizationServer.Data;
using AuthorizationServer.Interfaces;
using AuthorizationServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthorizationServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IUserRepository _userRepository;

        public AccountController(IIdentityServerInteractionService interaction, IUserRepository userRepository)
        {
            _interaction = interaction;
            _userRepository = userRepository;
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string button)
        {
            if (button != "login")
            {
                // the user clicked the "cancel" button
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                var user = _userRepository.FindByUsername(model.Username);

                if(user?.Password == model.Password)
                {
                    // await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));

                    // only set explicit expiration here if user chooses "remember me". 
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;

                    if (model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                        };
                    };

                    await HttpContext.SignInAsync(user.SubjectId.ToString(), user.Username, props);

                    // make sure the returnUrl is still valid, and if so redirect back to authorize endpoint or a local page
                    if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return Redirect("~/");
                }

                // await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(model);
        }
    }
}