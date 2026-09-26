using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace OrganizationManagement.Configuration.Permissions
{
    public class OrganizationPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                     "Organization", new List<PermissionDto>
                     {
                         new PermissionDto(OrganizationPermissions.SearchOrganization, "SearchOrganizations"),
                         new PermissionDto(OrganizationPermissions.ListOrganization, "ListOrganizations"),
                         new PermissionDto(OrganizationPermissions.CreateOrganization, "CreateOrganizations"),
                         new PermissionDto(OrganizationPermissions.EditOrganization, "EditOrganizations")
                     }
                },
                {
                    "OrganizationGroup", new List<PermissionDto>
                     {
                         new PermissionDto(OrganizationPermissions.SearchOrganizationGroup, "SearchOrganizationGroups"),
                         new PermissionDto(OrganizationPermissions.ListOrganizationGroup, "ListOrganizationGroups"),
                         new PermissionDto(OrganizationPermissions.CreateOrganizationGroup, "CreateOrganizationGroups"),
                         new PermissionDto(OrganizationPermissions.EditOrganizationGroup, "EditOrganizationGroups")
                     }
                }
               
            };
        }
    }
}
