using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Application.Contracts.OrganizationDepartment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationManagement.Application.Contracts.OrganizationPosition
{
    public class CreateOrganizationPosition
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }

        [Range(0, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ParentPositionId { get; set; }
        public List<OrganizationPositionViewModel> OrganizationPositions { get; set; }

        [Range(0, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long OrganizationDepartmentId { get; set; }
        public List<OrganizationDepartmentViewModel> OrganizationDepartments { get; set; }
        public List<OrganizationViewModel> Organizations { get; set; }

    }
}
