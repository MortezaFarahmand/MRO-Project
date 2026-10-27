using _0_Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role
{
    public class RoleQueryDto
    {
        // سطحهای مجاز برای انتخاب؛ در Application پر میشود
        public List<RoleScope> AllowedScopes { get; set; } = new(); // مثلاً {Account, Organization}
        // سقف دسترسی کاربرِ جاری (از IAuthHelper.MaxRoleScope)
        public RoleScope MaxScope { get; set; } = RoleScope.Site;
    }
}
