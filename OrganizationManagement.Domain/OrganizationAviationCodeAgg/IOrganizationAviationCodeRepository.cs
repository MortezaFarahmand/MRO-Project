
using _0_Framework.Domain;
using OrganizationManagement.Application.Contracts.OrganizationAviationCode;
using System.Collections.Generic;

namespace OrganizationManagement.Domain.OrganizationAviationCodeAgg
{
    public interface IOrganizationAviationCodeRepository : IRepository<long, OrganizationAviationCode>
    {
        List<OrganizationAviationCodeViewModel> GetOrganizationAviationCodes();
        EditOrganizationAviationCode GetDetails(long id);
        List<OrganizationAviationCodeViewModel> Search(OrganizationAviationCodeSearchModel searchModel);
    }
}
