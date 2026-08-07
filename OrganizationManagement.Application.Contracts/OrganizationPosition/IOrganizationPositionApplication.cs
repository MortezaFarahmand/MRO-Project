using _0_Framework.Application;
using System.Collections.Generic;

namespace OrganizationManagement.Application.Contracts.OrganizationPosition
{
    public interface IOrganizationPositionApplication
    {
        OperationResult Create(CreateOrganizationPosition command);
        OperationResult Edit(EditOrganizationPosition command);
        EditOrganizationPosition GetDetails(long id);
        List<OrganizationPositionViewModel> GetOrganizationPositions();
        List<OrganizationPositionViewModel> Search(OrganizationPositionSearchModel searchModel);
    }
}
