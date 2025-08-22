using System.Collections.Generic;
using _0_Framework.Domain;
using OrganizationManagement.Application.Contracts.OrganizationGroup;

namespace OrganizationManagement.Domain.OrganizationGroupAgg
{
    public interface IOrganizationGroupRepository : IRepository<long, OrganizationGroup>
    {
        List<OrganizationGroupViewModel> GetOrganizationGroups();
        EditOrganizationGroup GetDetails(long id);
        List<OrganizationGroupViewModel> Search(OrganizationGroupSearchModel searchModel);
    }
}
