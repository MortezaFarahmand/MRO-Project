using _0_Framework.Application;
using System.Collections.Generic;

namespace BasicDatanManagement.Application.Contracts.ApprovalAuthority
{
    public interface IApprovalAuthorityApplication
    {
        OperationResult Create(CreateApprovalAuthority command);
        OperationResult Edit(EditApprovalAuthority command);
        EditApprovalAuthority GetDetails(long id);
        List<ApprovalAuthorityViewModel> GetApprovalAuthoritys();
        List<ApprovalAuthorityViewModel> Search(ApprovalAuthoritySearchModel searchModel);
    }
}
