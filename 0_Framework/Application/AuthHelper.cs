using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public AuthViewModel CurrentAccountInfo()
        {
            var result = new AuthViewModel();
            if (!IsAuthenticated())
                return result;

            //var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            //result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
            //result.Username = claims.FirstOrDefault(x => x.Type == "Username").Value;
            //result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            //result.Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            //result.Role = Roles.GetRoleById(result.RoleId);
            //result.OrganizationId = long.Parse(claims.FirstOrDefault(x => x.Type == "OrganizationId").Value);
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();

            // Safely parse AccountId
            var accountIdClaim = claims.FirstOrDefault(x => x.Type == "AccountId")?.Value;
            result.Id = long.TryParse(accountIdClaim, out long accId) ? accId : 0; // Default to 0 if missing/invalid

            // Safely get Username
            result.Username = claims.FirstOrDefault(x => x.Type == "Username")?.Value ?? "Guest"; // Default string

            // Safely parse RoleId
            var roleIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            result.RoleId = long.TryParse(roleIdClaim, out long rId) ? rId : 0;

            // Safely get Fullname
            result.Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "Unknown User";

            // Get Role by Id (only if a valid RoleId was found)
            if (result.RoleId >= 0)
            {
                result.Role = Roles.GetRoleById(result.RoleId);
            }

            // Safely parse OrganizationId (Fixes your current crash)
            var orgIdClaim = claims.FirstOrDefault(x => x.Type == "OrganizationId")?.Value;
            result.OrganizationId = long.TryParse(orgIdClaim, out long oId) ? oId : 0;

            //result.ProfilePhoto = claims.FirstOrDefault(x => x.Type == "ProfilePhoto").Value;

            return result;
        }

        public List<int> GetPermissions()
        {
            if (!IsAuthenticated())
                return new List<int>();

            var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "permissions")
                ?.Value;
            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }

        public long CurrentAccountId()
        {
            return IsAuthenticated()
                ? long.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId")?.Value)
                : 0;
        }

        public string CurrentAccountMobile()
        {
            return IsAuthenticated()
                ? _contextAccessor.HttpContext.User.Claims.First(x => x.Type == "Mobile")?.Value
                : "";
        }

        public string CurrentAccountRole()
        {
            if (IsAuthenticated())
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            return null;
        }

        public long CurrentAccountOrganizationId()
        {
            return IsAuthenticated()
                ? long.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "OrganizationId")?.Value)
                : 0;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
            //var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            ////if (claims.Count > 0)
            ////    return true;
            ////return false;
            //return claims.Count > 0;
        }

        public void Signin(AuthViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions);  
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.Username), // Or Use ClaimTypes.NameIdentifier
                new Claim("OrganizationId", account.OrganizationId.ToString()),
                //new Claim("ProfilePhoto", account.ProfilePhoto)
                new Claim("permissions", permissions)
                //new Claim("Mobile", account.Mobile)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

    }
}