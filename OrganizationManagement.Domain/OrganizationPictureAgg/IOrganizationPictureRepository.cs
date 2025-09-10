using _0_Framework.Domain;
using OrganizationManagement.Application.Contracts.OrganizationPicture;
using System.Collections.Generic;

namespace OrganizationManagement.Domain.OrganizationPictureAgg
{
     public interface IOrganizationPictureRepository : IRepository<long, OrganizationPicture>
    {
        EditOrganizationPicture GetDetails(long id);
        List<OrganizationPictureViewModel> Search(OrganizationPictureSearchModel searchModel);
    }
}
