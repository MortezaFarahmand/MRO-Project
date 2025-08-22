using System.Collections.Generic;
using _0_Framework.Application;

namespace OrganizationManagement.Application.Contracts.OrganizationGroup
{
    public interface IOrganizationGroupApplication
    {
        OperationResult Create(CreateOrganizationGroup command);
        OperationResult Edit(EditOrganizationGroup command);
        EditOrganizationGroup GetDetails(long id);
        List<OrganizationGroupViewModel> GetOrganizationGroups();
        List<OrganizationGroupViewModel> Search(OrganizationGroupSearchModel searchModel);
    }
}
