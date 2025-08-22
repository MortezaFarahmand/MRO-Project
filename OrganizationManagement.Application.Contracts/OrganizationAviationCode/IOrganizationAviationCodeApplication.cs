
using _0_Framework.Application;
using System.Collections.Generic;

namespace OrganizationManagement.Application.Contracts.OrganizationAviationCode
{
    public interface IOrganizationAviationCodeApplication
    {
        OperationResult Create(CreateOrganizationAviationCode command);
        OperationResult Edit(EditOrganizationAviationCode command);
        EditOrganizationAviationCode GetDetails(long id);
        List<OrganizationAviationCodeViewModel> GetOrganizationAviationCodes();
        List<OrganizationAviationCodeViewModel> Search(OrganizationAviationCodeSearchModel searchModel);
        OperationResult IsEnable(long id);
        OperationResult IsDisable(long id);
    }
}
