using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.Organization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationManagement.Application.Contracts.OrganizationDepartment
{
    public class CreateOrganizationDepartment
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }

        public long ParentDepartmentId { get; set; }
        public List<OrganizationDepartmentViewModel> OrganizationDepartments { get; set; }

        [Range(0, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long OrganizationId { get; set; }

        public List<OrganizationViewModel> Organizations { get; set; }

    }
}
