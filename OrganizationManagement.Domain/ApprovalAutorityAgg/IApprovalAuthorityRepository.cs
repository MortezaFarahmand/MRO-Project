using _0_Framework.Domain;
using BasicDatanManagement.Application.Contracts.ApprovalAuthority;
using System.Collections.Generic;

namespace BasicDatanManagement.Domain.ApprovalAutorityAgg
{
    public interface IApprovalAuthorityRepository : IRepository<long, ApprovalAuthority>
    {
        EditApprovalAuthority GetDetails(long id);
        //ApprovalAuthority GetApprovalAutorityWithCountry(long id);
        List<ApprovalAuthorityViewModel> GetApprovalAutoritys();
        List<ApprovalAuthorityViewModel> Search(ApprovalAuthoritySearchModel searchModel);
    }
}
