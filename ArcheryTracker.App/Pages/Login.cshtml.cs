using System;
using System.Threading.Tasks;
using ArcheryTracker.App.Util;
using ArcheryTracker.Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArcheryTracker.App.Pages
{
    public class LoginModel : PageModel
    {
        private UserService _userService;
        private IdentityService _identityService;

        public LoginModel(UserService userService, IdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }
        
        public async Task OnGet(string redirectUri)
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties
            {
                RedirectUri = redirectUri
            });
        }
    }
}
