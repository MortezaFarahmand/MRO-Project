using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Permission> Permissions { get; private set; } = new List<Permission>();
        public List<AccountRole> AccountRoles { get; private set; } = new List<AccountRole>();
        public RoleScope Scope { get; private set; }
        // اگر نقش سطح Admin نباشد، «فقط ادمین» نیست
        public bool IsSystemAdminOnly => Scope == RoleScope.Admin;

        protected Role()
        {

        }

        public Role(string name, List<Permission> permissions, RoleScope scope)
        {
            Name = name;
            Permissions = permissions ?? new();
            Scope = scope;
        }

        public void Edit(string name, List<Permission> permissions, RoleScope scope)
        {
            Name = name;
            Permissions = permissions ?? Permissions;
            Scope = scope;
        }
    }
}
