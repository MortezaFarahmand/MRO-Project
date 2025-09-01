
using _0_Framework.Domain;
using BasicDatanManagement.Application.Contracts.OrganizationAviationCode;
using System.Collections.Generic;

namespace BasicDatanManagement.Domain.OrganizationAviationCodeAgg
{
    public interface IOrganizationAviationCodeRepository : IRepository<long, OrganizationAviationCode>
    {
        List<OrganizationAviationCodeViewModel> GetOrganizationAviationCodes();
        EditOrganizationAviationCode GetDetails(long id);
        List<OrganizationAviationCodeViewModel> Search(OrganizationAviationCodeSearchModel searchModel);
    }
}
