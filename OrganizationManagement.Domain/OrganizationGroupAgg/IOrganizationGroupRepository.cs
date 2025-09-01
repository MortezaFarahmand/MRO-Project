using System.Collections.Generic;
using _0_Framework.Domain;
using BasicDatanManagement.Application.Contracts.OrganizationGroup;

namespace BasicDatanManagement.Domain.OrganizationGroupAgg
{
    public interface IOrganizationGroupRepository : IRepository<long, OrganizationGroup>
    {
        List<OrganizationGroupViewModel> GetOrganizationGroups();
        EditOrganizationGroup GetDetails(long id);
        List<OrganizationGroupViewModel> Search(OrganizationGroupSearchModel searchModel);
    }
}
