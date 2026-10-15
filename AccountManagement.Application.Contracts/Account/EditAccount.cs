using _0_Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount : RegisterAccount
    {
        public long Id { get; set; }
        public List<PermissionDto> MappedAccountPermissions { get; set; } = new();
    }
}
