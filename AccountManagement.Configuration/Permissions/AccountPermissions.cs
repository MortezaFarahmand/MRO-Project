namespace AccountManagement.Configuration.Permissions
{
    public static class AccountPermissions
    {
        //Accounts
        public const int ListAccounts = 1;
        public const int SearchAccounts = 2;
        
        public static int CreateAccounts = 3;
        public static int EditAccounts = 4;

        public static int AccountOrganizationId = 5;

        //Roles
        public const int ListRoles = 8;
        public const int CreateRoles = 9;
        public const int EditRoles = 10;
    }
}
