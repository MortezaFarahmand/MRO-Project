using System;
using System.Collections.Generic;
using System.Linq;

namespace _0_Framework.Infrastructure
{
    public enum RoleScope
    {
        Site = 1,  // پایینترین (کاربر ساده)
        System = 2,
        Admin = 3   // بالاترین
    }

    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string SystemUser = "SystemUser";
        public const string SiteUser = "SiteUser";

        private static readonly Dictionary<string, RoleScope> RoleScopes = new()
        {
            [SiteUser] = RoleScope.Site,
            [SystemUser] = RoleScope.System,
            [Administrator] = RoleScope.Admin
        };

        public static string GetRoleById(long roleId) => roleId switch
        {
            1 => Administrator,
            2 => SystemUser,
            3 => SiteUser,
            _ => throw new ArgumentOutOfRangeException(nameof(roleId))
        };

        public static RoleScope? GetScope(string roleName) =>
            RoleScopes.TryGetValue(roleName, out var scope) ? scope : null;
        
        /// بالاترین Scope را از لیست Scopeها برمی‌گرداند.
        /// اگر لیست خالی باشد → Site (امن‌ترین حالت)
        public static RoleScope GetMaxScope(IEnumerable<RoleScope> scopes)
        {
            if (scopes == null || !scopes.Any())
                return RoleScope.Site;

            return scopes.Max();
        }

        /// بالاترین Scope را از لیست نام نقش‌ها محاسبه می‌کند.
        public static RoleScope GetMaxScope(IEnumerable<string> roleNames)
        {
            if (roleNames == null || !roleNames.Any())
                return RoleScope.Site;

            var scopes = roleNames
                .Select(GetScope)
                .Where(s => s.HasValue)
                .Select(s => s.Value);

            return GetMaxScope(scopes);
        }

        

        // قاعده اصلی: آیا کاربرِ currentUserRole مجاز به دیدن/مدیریت targetRole است؟
        public static bool CanAccess(string currentUserRole, string targetRole)
        {
            var current = GetScope(currentUserRole);
            var target = GetScope(targetRole);
            if (current is null || target is null) return false; // ناشناخته = بدون دسترسی
            return target <= current;
        }

        // برای فیلتر کردن نقشهای قابل انتخاب در UI/کوئری
        public static RoleScope GetMaxScope(string currentUserRole) =>
            GetScope(currentUserRole) ?? RoleScope.Site;          // پیشفرض امن: پایینترین

        public static IEnumerable<RoleScope> GetAllowedScopes(RoleScope maxScope)
        {
            return Enum.GetValues<RoleScope>().Where(s => s <= maxScope);
        }

        public static IEnumerable<RoleScope> GetAllowedScopes(IEnumerable<RoleScope> currentScopes)
        {
            var max = GetMaxScope(currentScopes);
            return Enum.GetValues<RoleScope>().Where(s => s <= max);
        }

        // کمکی برای کار با موجودیت: آیا کاربر جاری می‌تواند نقشی با این سطح را مدیریت کند؟
        public static bool CanManageScope(string currentUserRole, RoleScope targetScope) =>
             targetScope <= GetMaxScope(currentUserRole);

        public static bool CanManageScope(IEnumerable<RoleScope> currentScopes, RoleScope targetScope) =>
            targetScope <= GetMaxScope(currentScopes);
    }
}
