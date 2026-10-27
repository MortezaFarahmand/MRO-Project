using _0_Framework.Domain;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        //public long RoleId { get; private set; }
        public long? OrganizationId { get; private set; }
        //public Role Role { get; private set; }
        public bool? IsSystemAdmin { get; private set; }
        public bool IsOrganizationAdmin { get; private set; }
        public ICollection<AccountRole> AccountRoles { get; private set; } = new List<AccountRole>();
        public string ProfilePhoto { get; private set; }

        public Account()
        {

        }
        public Account(string fullname, string username, string password,
            string mobile, string profilePhoto, long? organizationId)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            Mobile = mobile;
            ProfilePhoto = profilePhoto;
            OrganizationId = organizationId;
            IsSystemAdmin = false;
            IsOrganizationAdmin = false;
        }

        public void Edit(string fullname, string username,
            string mobile, string profilePhoto, long? organizationId, 
            bool? isSystemAdmin, bool isOrganizationAdmin)
        {
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            OrganizationId = organizationId;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;
            IsSystemAdmin = isSystemAdmin;
            IsOrganizationAdmin = isOrganizationAdmin;
            //AccountRoles = accountRoles;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void AddRoles(AccountRole accountRole)
        {
            AccountRoles.Add(accountRole);
        }

        public void SetRoles(List<AccountRole> roles)
        {
            AccountRoles.Clear();
            foreach (var r in roles)
                AccountRoles.Add(r);
        }
    }
}
