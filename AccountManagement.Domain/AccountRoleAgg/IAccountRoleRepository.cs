using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountRoleAgg
{
    public interface IAccountRoleRepository : IRepository<long, AccountRole>
    {
        //List<AccountRoleViewModel> List();
        //EditAccountRole GetDetails(long id);
        //List<AccountRoleViewModel> GetByAccountId(long id);
    }
}
