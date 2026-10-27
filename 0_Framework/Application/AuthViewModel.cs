using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public List<long> RoleIds { get; set; } = new();
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string ProfilePhoto { get; set; }
        public long? OrganizationId { get; set; }
        public bool? IsSystemAdmin { get; set; }
        public bool IsOrganizationAdmin { get; set; }

        public List<long> Permissions { get; set; } = new();
        public List<RoleScope> RoleScopes { get; set; } = new();

        public AuthViewModel()
        {
        }

        public AuthViewModel(long id, List<long> roleIds, string fullname, string username
            , List<long> permissions, long? organizationId, bool? isSystemAdmin, bool isOrganizationAdmin)
        {
            Id = id;
            RoleIds = roleIds;
            Fullname = fullname;
            Username = username;
            //ProfilePhoto = profilePhoto;
            //Mobile = mobile;
            Permissions = permissions;
            OrganizationId = organizationId;
            IsSystemAdmin = isSystemAdmin;
            IsOrganizationAdmin = isOrganizationAdmin;
        }
    }
}