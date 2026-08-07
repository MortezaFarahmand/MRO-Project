using _0_Framework.Domain;
using OrganizationManagement.Application.Contracts.OrganizationPosition;
using System.Collections.Generic;


namespace OrganizationManagement.Domain.OrganizationPositionAgg
{
    public interface IOrganizationPositionRepository : IRepository<long, OrganizationPosition>
    {
        List<OrganizationPositionViewModel> GetOrganizationPositions();
        EditOrganizationPosition GetDetails(long id);
        List<OrganizationPositionViewModel> Search(OrganizationPositionSearchModel searchModel);
    }
}
