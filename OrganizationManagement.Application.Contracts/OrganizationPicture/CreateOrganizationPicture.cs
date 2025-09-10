
using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.Organization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationManagement.Application.Contracts.OrganizationPicture
{
    public class CreateOrganizationPicture
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long OrganizationId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]    
        public string Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }

        public List<OrganizationViewModel> Organizations { get; set; }
    }
}
