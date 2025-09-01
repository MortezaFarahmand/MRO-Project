using _0_Framework.Domain;
using BasicDatanManagement.Application.Contracts.OrganizationPicture;
using System.Collections.Generic;

namespace BasicDatanManagement.Domain.OrganizationPictureAgg
{
     public interface IOrganizationPictureRepository : IRepository<long, OrganizationPicture>
    {
        EditOrganizationPicture GetDetails(long id);
        List<OrganizationPictureViewModel> Search(OrganizationPictureSearchModel searchModel);
    }
}
