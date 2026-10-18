using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class AccountRole
    {
        public long Id { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        public long AccountId { get; private set; }
        public Account Account { get; private set; }


        public AccountRole(long accountId, long roleId)
        {
            AccountId = accountId;
            //if(RoleId == 0)
            //    RoleId = 2;
            RoleId = roleId;
        }


    }
}
