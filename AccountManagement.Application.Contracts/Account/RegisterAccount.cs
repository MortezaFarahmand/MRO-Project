using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;
using OrganizationManagement.Application.Contracts.Organization;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace AccountManagement.Application.Contracts.Account
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }

        [Range(0, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }

        //public long RoleId { get; set; }


        public IFormFile ProfilePhoto { get; set; }

        public List<RoleViewModel> Roles { get; set; } = new();
        public List<long> RoleIds { get; set; } = new();

        public long? OrganizationId { get; set; }
        public List<OrganizationViewModel> Organizations { get; set; } = new List<OrganizationViewModel>();

        //public List<long> AccountRoles { get; set; } = new List<long>();


        //public List<int> AccountPermissions { get; set; } = new List<int>();
    }
}
