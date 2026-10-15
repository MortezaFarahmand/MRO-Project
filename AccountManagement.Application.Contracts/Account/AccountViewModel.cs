using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Application.Contracts.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        //public long RoleId { get; set; }
        public List<RoleViewModel> Roles { get; set; } = new();
        public long AccountId { get; set; }
        public string Account { get; set; }
        public string ProfilePhoto { get; set; }
        public string CreationDate { get; set; }
        public long OrganizationId { get; set; }
        public string Organization { get; set; }
    }
}
