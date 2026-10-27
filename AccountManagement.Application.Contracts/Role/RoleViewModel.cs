using _0_Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role
{
    public class RoleViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public List<long> PermissionIds { get; set; } = new();
        public List<PermissionDto> PermissionCodeNames { get; set; } = new();
        public RoleScope Scope { get; set; }
        public bool IsSystemAdminOnly => Scope == RoleScope.Admin; // اختیاری برای UI

        public List<RoleScope> AllowedScopes { get; set; }
    }
}
