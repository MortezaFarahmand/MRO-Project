using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public Account GetBy(string username)
        {
            return _context.Accounts
                .Include(x => x.AccountRoles)
                    .ThenInclude(ar => ar.Role)
                        .ThenInclude(r => r.Permissions)
                .FirstOrDefault(x => x.Username == username);
            //return _context.Accounts.FirstOrDefault(x => x.Username == username);
        }

        public EditAccount GetDetails(long id)
        {
            var account = _context.Accounts
        .AsNoTracking()
        .Include(x => x.AccountRoles)
            .ThenInclude(ar => ar.Role)
                .ThenInclude(r => r.Permissions)
        .FirstOrDefault(x => x.Id == id);

            if (account == null)
                return null;

            return new EditAccount
            {
                Id = account.Id,
                Fullname = account.Fullname,
                Username = account.Username,
                Mobile = account.Mobile,
                OrganizationId = account.OrganizationId,
                RoleIds = account.AccountRoles.Select(ar => ar.RoleId).ToList(),
                IsSystemAdmin = account.IsSystemAdmin,
                IsOrganizationAdmin = account.IsOrganizationAdmin,
                MappedAccountPermissions = account.AccountRoles
                    .SelectMany(ar => ar.Role.Permissions)
                    .DistinctBy(p => p.Code)
                    .Select(p => new PermissionDto(p.Code, p.Name))
                    .ToList()
            };
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel, long? currentOrgId)
        {
            var query = _context.Accounts.AsQueryable();
            // IsSystemAdmin Check...
            if (!searchModel.IsSystemAdmin)
                query = query.Where(x => x.OrganizationId == currentOrgId);
            else if(searchModel.IsSystemAdmin && searchModel.OrganizationId.HasValue 
                                                    && searchModel.OrganizationId != 0)
                query = query.Where(x => x.OrganizationId == searchModel.OrganizationId);
            ////////////////////////
            if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
                query = query.Where(x => x.Fullname.Contains(searchModel.Fullname));

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
                query = query.Where(x => x.Username.Contains(searchModel.Username));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            return query.OrderByDescending(x => x.Id)
                .Include(ar => ar.AccountRoles)
                .ThenInclude(r => r.Role)
                    .ThenInclude(p => p.Permissions)
                .Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    Fullname = x.Fullname,
                    Mobile = x.Mobile,
                    ProfilePhoto = x.ProfilePhoto,
                    Roles = x.AccountRoles.Select(ar => new RoleViewModel
                    {
                        Id = ar.RoleId,
                        Name = ar.Role.Name,
                        // پر کردن مستقیم لیست سطح دوم بدون نیاز به متد خارجی
                        PermissionCodeNames = ar.Role.Permissions.Select(p => new PermissionDto
                        {
                            Code = p.Code,
                            Name = p.Name
                        }).ToList()
                    }).ToList(),
                    Username = x.Username,
                    CreationDate = x.CreationDate.ToShortDateString(),
                    OrganizationId = x.OrganizationId,
                    IsSyatemAdmin = x.IsSystemAdmin,
                    IsOrganizationAdmin = x.IsOrganizationAdmin
                }).ToList();

        }
    }
}
