using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string  Name { get; private set; }
        public List<Permission> Permissions { get; private set; } = new();
        public ICollection<AccountRole> AccountRoles { get; private set; }

        protected Role() 
        { 

        }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        public void Edit(string name, List<Permission> permisions)
        {
            Name = name;
            Permissions = permisions;
        }
    }
}
