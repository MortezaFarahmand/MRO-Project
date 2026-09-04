using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }

        [Range(0, long.MaxValue , ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long RoleId { get; set; }


        public IFormFile ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
