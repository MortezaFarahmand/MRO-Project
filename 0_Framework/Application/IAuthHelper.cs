using _0_Framework.Infrastructure;
using System;
using System.Collections.Generic;

namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthenticated();
        void Signin(AuthViewModel account);
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        List<long> GetPermissions();
        long CurrentAccountId();
        string CurrentAccountMobile();
        long? CurrentAccountOrganizationId();
        List<long> CurrentAccountRoles();
        long? SetOrganizationId(long? organizationId);
        bool CurrentAccountIsAdmin();
        bool? CurrentAccountIsSystemAdminOrNull();
        bool CurrentAccountIsOrganizationAdmin();
        Guid AccountId { get; }
        RoleScope MaxRoleScope { get; }   // همان خروجی Roles.GetMaxScope
        string CurrentRoleName { get; }
    }
}
