using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.Entiti;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicDataManagement.Application.Contracts.PictureCategory
{
    public class CreatePictureCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public string Remark { get; set; }
        public long EntitiId { get; set; }
        public List<EntitiViewModel> Entities { get; set; }
    }
}
