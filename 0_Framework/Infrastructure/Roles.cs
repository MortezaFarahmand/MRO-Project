namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string SiteUser = "2";
        public const string SystemUser = "3";

        public static string GetRoleById(long id)
        {
            switch (id)
            {
                case 1:
                    return "Administrator";
                case 3:
                    return "System User";
                default:
                    return "";
            }
        }
    }
}
