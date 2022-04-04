using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArcheryTracker.Logic.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace ArcheryTracker.App.Util
{
    public class IdentityService
    {
        private const string IdClaimName = "nameidentifier";
        private const string EmailClaimName = "emailaddress";
        private const string NicknameClaim = "nickname";
        private const string FirstTimeLoginClaimName = "freshSignup";

        public Auth0User GetLoggedInUser(AuthenticationState authState, bool freshSignup)
        {
            var userIdentity = authState.User;
            var userName = freshSignup ? GetClaim(userIdentity, NicknameClaim) : userIdentity.Identity.Name;
            var userId = GetClaim(userIdentity, IdClaimName);
            var userEmail = GetClaim(userIdentity, EmailClaimName);
            
            return new Auth0User(userId, userName, userEmail);
        }

        public string GetLoggedInUserId(AuthenticationState authState)
        {
            return GetClaim(authState.User, IdClaimName);
        }

        public bool IsFirstLogin(AuthenticationState authState)
        {
            return Boolean.Parse(GetClaim(authState.User, FirstTimeLoginClaimName));
        }
        
        private string GetClaim(ClaimsPrincipal userIdentity, string claimName)
        {
            return ParseClaim(claimName, userIdentity.Claims);
        }
        
        private static string ParseClaim(string claimName, IEnumerable<Claim> claims)
        {
            var claim = claims.FirstOrDefault(c => c.Type.Contains(claimName));
            return claim?.Value;
        }
    }
}