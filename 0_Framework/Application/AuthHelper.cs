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

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();

            var accountIdClaim = claims.FirstOrDefault(x => x.Type == "AccountId")?.Value;
            result.Id = long.TryParse(accountIdClaim, out long accId) ? accId : 0;

            result.Username = claims.FirstOrDefault(x => x.Type == "Username")?.Value ?? "Guest";
            result.Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "Unknown User";

            var orgIdClaim = claims.FirstOrDefault(x => x.Type == "OrganizationId")?.Value;
            result.OrganizationId = long.TryParse(orgIdClaim, out long oId) ? oId : 0;

            // نقش‌ها: اول کلیم جدید roles (آرایه JSON)، اگر نبود fallback به ClaimTypes.Role تکی قدیمی
            result.RoleIds = new List<long>();
            var rolesJson = claims.FirstOrDefault(x => x.Type == "roles")?.Value;
            if (!string.IsNullOrWhiteSpace(rolesJson))
            {
                try { result.RoleIds = JsonConvert.DeserializeObject<List<long>>(rolesJson) ?? new List<long>(); }
                catch { result.RoleIds = new List<long>(); }
            }
            if (result.RoleIds.Count == 0)
            {
                // سازگاری با کوکی قدیمی + چند Claim تکراری Role
                foreach (var rc in claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value))
                {
                    if (long.TryParse(rc, out long rId))
                        result.RoleIds.Add(rId);
                }
            }

            // دسترسی‌ها
            result.Permissions = GetPermissions();

            // برای نمایش (فقط نقش اول)
            if (result.RoleIds.Count > 0)
                result.Role = Roles.GetRoleById(result.RoleIds[0]);

            return result;
        }

        public List<int> GetPermissions()
        {
            if (!IsAuthenticated())
                return new List<int>();

            var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "permissions")?.Value;
            if (string.IsNullOrWhiteSpace(permissions))
                return new List<int>();
            try
            {
                return JsonConvert.DeserializeObject<List<int>>(permissions) ?? new List<int>();
            }
            catch
            {
                return new List<int>();
            }
        }

        public long CurrentAccountId()
        {
            if (!IsAuthenticated()) return 0;
            var v = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "AccountId")?.Value;
            return long.TryParse(v, out long id) ? id : 0;
        }

        public string CurrentAccountMobile()
        {
            if (!IsAuthenticated()) return "";
            // کلیم Mobile در Signin ست نمی‌شود، پس خالی برمی‌گردانیم تا خطا ندهد
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Mobile")?.Value ?? "";
        }

        public string CurrentAccountRole()
        {
            if (IsAuthenticated())
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            return null;
        }

        public List<long> CurrentAccountRoles()
        {
            if (!IsAuthenticated()) return new List<long>();
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            var rolesJson = claims.FirstOrDefault(x => x.Type == "roles")?.Value;
            if (!string.IsNullOrWhiteSpace(rolesJson))
            {
                try
                {
                    var list = JsonConvert.DeserializeObject<List<long>>(rolesJson);
                    if (list != null && list.Count > 0) return list;
                }
                catch { }
            }
            return claims.Where(x => x.Type == ClaimTypes.Role)
                .Select(x => long.TryParse(x.Value, out long r) ? (long?)r : null)
                .Where(x => x.HasValue).Select(x => x.Value).ToList();
        }

        public long CurrentAccountOrganizationId()
        {
            if (!IsAuthenticated()) return 0;
            var v = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "OrganizationId")?.Value;
            return long.TryParse(v, out long id) ? id : 0;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;
        }

        public void Signin(AuthViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions ?? new List<int>());
            var roles = JsonConvert.SerializeObject(account.RoleIds ?? new List<long>());

            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname ?? ""),
                new Claim("Username", account.Username ?? ""),
                new Claim("OrganizationId", account.OrganizationId.ToString()),
                new Claim("permissions", permissions),
                new Claim("roles", roles)
            };

            // برای سازگاری با کدهای قدیمی که فقط ClaimTypes.Role تکی می‌خوانند
            foreach (var roleId in (account.RoleIds ?? new List<long>()))
                claims.Add(new Claim(ClaimTypes.Role, roleId.ToString()));

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
