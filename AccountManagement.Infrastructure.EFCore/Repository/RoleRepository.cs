using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _accountContext;

        public RoleRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public List<RoleViewModel> GetRoles(RoleQueryDto query)
        {
            var roles = _accountContext.Roles
                .AsNoTracking()
                .Where(r => r.Scope <= query.MaxScope)
                .Include(r => r.Permissions)
                .ToList();

            return roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                CreationDate = r.CreationDate.ToShortDateString(),
                Scope = r.Scope,
                PermissionIds = r.Permissions.Select(p => p.Id).ToList(),
                PermissionCodeNames = r.Permissions
                    .Select(p => new PermissionDto(p.Code, p.Name))
                    .ToList()
            }).ToList();
        }

        public EditRole GetDetails(long id, RoleQueryDto query)
        {
            var role = _accountContext.Roles
                .AsNoTracking()
                .Include(r => r.Permissions)
                .FirstOrDefault(r => r.Id == id && r.Scope <= query.MaxScope);

            if (role == null)
                return null;

            return new EditRole
            {
                Id = role.Id,
                Name = role.Name,
                Scope = role.Scope,
                PermissionCodes = role.Permissions.Select(p => p.Code).ToList(),
                MappedPermissions = role.Permissions
                    .Select(p => new PermissionDto(p.Code, p.Name))
                    .ToList()
            };
        }

        public Role GetForEdit(long id, RoleQueryDto query)
        {
            return _accountContext.Roles
                .Include(r => r.Permissions)
                .FirstOrDefault(r => r.Id == id && r.Scope <= query.MaxScope);
        }
    }
}

