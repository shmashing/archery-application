using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace ArcheryTracker.App.Util
{
    public class IdentityService
    {
        private const string IdClaimName = "nameidentifier";
        private readonly AuthenticationStateProvider _authProvider;

        public IdentityService(AuthenticationStateProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public async Task<string> GetLoggedInUserId()
        {    
            return await GetClaim(IdClaimName);
        }

        private async Task<string> GetClaim(string claimName)
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var userIdentity = authState.User;
            return ParseClaim(claimName, userIdentity.Claims);
        }
        
        private static string ParseClaim(string claimName, IEnumerable<Claim> claims)
        {
            var claim = claims.FirstOrDefault(c => c.Type.Contains(claimName));
            return claim?.Value;
        }
    }
}