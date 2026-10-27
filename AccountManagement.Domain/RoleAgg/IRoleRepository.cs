using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<long, Role>
    {
        List<RoleViewModel> GetRoles(RoleQueryDto query);
        EditRole GetDetails(long id, RoleQueryDto query);
        Role GetForEdit(long id, RoleQueryDto query);   // برای Edit واقعی
    }
}
