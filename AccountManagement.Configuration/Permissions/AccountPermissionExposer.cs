using _0_Framework.Infrastructure;
namespace AccountManagement.Configuration.Permissions
{
    public class AccountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                     "account", new List<PermissionDto>
                     {
                         new PermissionDto(AccountPermissions.SearchAccounts, "SearchAccounts"),
                         new PermissionDto(AccountPermissions.ListAccounts, "ListAccounts"),
                         new PermissionDto(AccountPermissions.CreateAccounts, "CreateAccounts"),
                         new PermissionDto(AccountPermissions.EditAccounts, "EditAccounts"),

                         new PermissionDto(AccountPermissions.AccountOrganizationId, "AccountOrganizationId"),
                     }
                },
                {
                     "role", new List<PermissionDto>
                     {
                         new PermissionDto(AccountPermissions.ListRoles, "ListRoles"),
                         new PermissionDto(AccountPermissions.CreateRoles, "CreateRoles"),
                         new PermissionDto(AccountPermissions.EditRoles, "EditRoles"),
                     }
                },

            };
        }
    }
}
