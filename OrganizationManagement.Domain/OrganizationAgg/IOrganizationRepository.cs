using _0_Framework.Domain;
using BasicDatanManagement.Application.Contracts.Organization;
using System.Collections.Generic;

namespace BasicDatanManagement.Domain.OrganizationAgg
{
    public interface IOrganizationRepository : IRepository<long, Organization>
    {
        EditOrganization GetDetails(long id);
        List<OrganizationViewModel> GetOrganizations();
        List<OrganizationViewModel> Search(OrganizationSearchModel searchModel);
    }
}
