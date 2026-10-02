using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PartManagement.Application.Contracts.Basic_data.EquipmentType
{
    public class CreateEquipmentType
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }

        public string ColorCode { get; set; }

        public string Description { get; set; }

        public string Picture {  get; set; }
        public string CreationDate { get; set; }
    }
}
