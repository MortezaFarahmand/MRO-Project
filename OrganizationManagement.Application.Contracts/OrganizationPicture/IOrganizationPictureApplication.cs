
using _0_Framework.Application;
using System.Collections.Generic;

namespace OrganizationManagement.Application.Contracts.OrganizationPicture
{
    public interface IOrganizationPictureApplication
    {
        OperationResult Create(CreateOrganizationPicture command);
        OperationResult Edit(EditOrganizationPicture command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditOrganizationPicture GetDetails(long id);
        List<OrganizationPictureViewModel> Search(OrganizationPictureSearchModel searchModel);
    }
}
