
using _0_Framework.Application;
using System.Collections.Generic;

namespace OrganizationManagement.Application.Contracts.Organization
{
    public interface IOrganizationApplication 
    {
        OperationResult Create(CreateOrganization command);
        OperationResult Edit(EditOrganization command);
        OperationResult Active(long id);
        OperationResult NotActive(long id);
        EditOrganization GetDetails(long id);
        List<OrganizationViewModel> GetOrganizations();
        List<OrganizationViewModel> Search(OrganizationSearchModel searchModel);
    }
}
