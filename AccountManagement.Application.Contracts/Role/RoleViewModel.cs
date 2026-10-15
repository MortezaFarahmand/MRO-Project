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
    }
}
