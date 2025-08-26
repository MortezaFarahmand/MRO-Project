using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
