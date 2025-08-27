using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Application.Contracts.PersonGroup
{
    public class CreatePersonGroup
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }

        public string Remark { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public bool IsActive { get; set; }
    }
}
