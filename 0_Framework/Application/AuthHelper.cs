using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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

            var isSystemAdminClaim = claims.FirstOrDefault(x => x.Type == "IsSystemAdmin")?.Value;
            result.IsSystemAdmin = bool.TryParse(isSystemAdminClaim, out var isSystemAdmin) && isSystemAdmin;

            var isOrganizationAdminClaim = claims.FirstOrDefault(x => x.Type == "IsOrganizationAdmin")?.Value;
            result.IsOrganizationAdmin = bool.TryParse(isOrganizationAdminClaim, out var isOrganizationAdmin) && isOrganizationAdmin;

            result.Username = claims.FirstOrDefault(x => x.Type == "Username")?.Value ?? "Guest";
            result.Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "Unknown User";
            result.Mobile = claims.FirstOrDefault(x => x.Type == "Mobile")?.Value ?? "";

            var orgIdClaim = claims.FirstOrDefault(x => x.Type == "OrganizationId")?.Value;
            result.OrganizationId = long.TryParse(orgIdClaim, out long oId) ? oId : (long?)null;

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

        public Guid AccountId
        {
            get
            {
                long id = CurrentAccountInfo().Id;
                return Guid.TryParse(id.ToString(), out var g) ? g : Guid.Empty;
            }
        }

        public string CurrentRoleName => CurrentAccountInfo().Role;

        public RoleScope MaxRoleScope
        {
            get
            {
                if (!IsAuthenticated())
                    return RoleScope.Site;

                // اولویت با Claim مخصوص roleScopes
                var scopesJson = _contextAccessor.HttpContext.User
                    .FindFirst("roleScopes")?.Value;

                if (!string.IsNullOrWhiteSpace(scopesJson))
                {
                    try
                    {
                        var scopes = JsonConvert.DeserializeObject<List<RoleScope>>(scopesJson);
                        return Roles.GetMaxScope(scopes);
                    }
                    catch { /* ignore */ }
                }

                // Fallback: اگر roleScopes نبود، از نام نقش‌ها استفاده کن (سازگاری با کوکی‌های قدیمی)
                var roleNames = CurrentAccountRoles()
                    .Select(id =>
                    {
                        try { return Roles.GetRoleById(id); }
                        catch { return null; }
                    })
                    .Where(n => n != null);

                return Roles.GetMaxScope(roleNames);
            }
        }

        public List<long> GetPermissions()
        {
            if (!IsAuthenticated())
                return new List<long>();

            var permissions = _contextAccessor.HttpContext.User
                .Claims.FirstOrDefault(x => x.Type == "permissions")?.Value;

            if (string.IsNullOrWhiteSpace(permissions))
                return new List<long>();
            try
            {
                return JsonConvert.DeserializeObject<List<long>>(permissions) ?? new List<long>();
            }
            catch
            {
                return new List<long>();
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

        public long? CurrentAccountOrganizationId()
        {
            var value = _contextAccessor.HttpContext?.User.FindFirst("OrganizationId")?.Value;
            return long.TryParse(value, out var id) && id >= 0 ? id : (long?)null;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;
        }

        public void Signin(AuthViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions ?? new List<long>());
            var roles = JsonConvert.SerializeObject(account.RoleIds ?? new List<long>());
            var roleScopes = JsonConvert.SerializeObject(account.RoleScopes ?? new List<RoleScope>());

            var claims = new List<Claim>
    {
        new Claim("AccountId", account.Id.ToString()),
        new Claim(ClaimTypes.Name, account.Fullname ?? ""),
        new Claim("Username", account.Username ?? ""),
        new Claim("OrganizationId", account.OrganizationId?.ToString() ?? ""),
        new Claim("permissions", permissions),
        new Claim("roles", roles),
        new Claim("roleScopes", roleScopes),
        new Claim("IsSystemAdmin", account.IsSystemAdmin?.ToString() ?? "false"),
        new Claim("IsOrganizationAdmin", account.IsOrganizationAdmin.ToString())
    };

            // ===== اصلاح اصلی اینجاست =====
            if (account.RoleIds != null)
            {
                foreach (var roleId in account.RoleIds)
                {
                    // نام نقش را برای پالیسی‌ها اضافه کن
                    try
                    {
                        var roleName = Roles.GetRoleById(roleId);
                        claims.Add(new Claim(ClaimTypes.Role, roleName));
                    }
                    catch
                    {
                        // اگر نقش ناشناخته بود، Id را به عنوان fallback بگذار
                        claims.Add(new Claim(ClaimTypes.Role, roleId.ToString()));
                    }
                }
            }

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

        /// بررسی می‌کند که آیدی سازمانی داده‌شده برای اکانت وارد‌شده معتبر/مجاز است یا نه
        /// و آیدی نهایی که باید استفاده شود را برمی‌گرداند.
        public long? SetOrganizationId(long? organizationId)
        {
            // اگر آیدی اکانت نال یا آیدی داده شده به متد صفر و یا اکانت ادمین نباشد 
            // متد مقدار صفر را برمی گرداند  - وارد شرطها نمی شود
            //  در صورتی که آیدی سازمانی اکانت وارد شده به سیستم غیرصفر باشد و نال نباشد
            // متد مقدار آیدی سازمان اکانت وارد شده را برمیگرداند
            // در غیر آن صورت یعنی اگر اکانت ادمین سیستم باشد متد
            // همان مقداری را که گرفته است چه صفر چه غیرصفر و چه نال را برمیگرداند

            var currentOrgId = CurrentAccountOrganizationId();
            bool? isSysAdmin = CurrentAccountIsSystemAdminOrNull();

            if (currentOrgId.HasValue && currentOrgId.Value != 0)
                return currentOrgId.Value;
            if (isSysAdmin == true)
                return organizationId;
            return 0;
        }

        public bool CurrentAccountIsAdmin()
        {
            var currentOrgId = CurrentAccountOrganizationId();

            if (currentOrgId.HasValue && currentOrgId.Value != 0)
                return false;

            return true;
        }

        public bool? CurrentAccountIsSystemAdminOrNull()
        {
            if (!IsAuthenticated())
                return null;

            var claim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IsSystemAdmin");
            if (claim == null)
                return null;

            return bool.TryParse(claim.Value, out var result) ? result : (bool?)null;
        }

        public bool CurrentAccountIsOrganizationAdmin()
        {
            if (!IsAuthenticated()) return false;
            var v = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "IsOrganizationAdmin")?.Value;
            return bool.TryParse(v, out bool b) ? b : false;
        }
    }
}
