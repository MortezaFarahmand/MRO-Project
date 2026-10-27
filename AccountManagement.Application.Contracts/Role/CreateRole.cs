using _0_Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role
{
    public class CreateRole
    {
        public string Name { get; set; }
        public RoleScope Scope { get; set; }
        public List<long> PermissionCodes { get; set; } = new();
        public List<RoleScope> AllowedScopes { get; set; } // ✅ سطحهای مجاز برای دراپ‌داون (Site..Max)
    }
}
