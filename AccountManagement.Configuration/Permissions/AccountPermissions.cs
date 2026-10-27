namespace AccountManagement.Configuration.Permissions
{
    public static class AccountPermissions
    {
        //Accounts
        public const long ListAccounts = 1;
        public const long SearchAccounts = 2;

        public static long CreateAccounts = 3;
        public static long EditAccounts = 4;

        public static long AccountOrganizationId = 5;

        //Roles
        public const long ListRoles = 7;
        public const long CreateRoles = 8;
        public const long EditRoles = 9;
    }
}
