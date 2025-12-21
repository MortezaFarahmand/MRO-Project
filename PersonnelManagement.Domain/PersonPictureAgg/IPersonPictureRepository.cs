using _0_Framework.Domain;
using PersonnelManagement.Application.Contracts.PersonPicture;
using System.Collections.Generic;

namespace PersonnelManagement.Domain.PersonPictureAgg
{
    public interface IPersonPictureRepository : IRepository<long, PersonPicture>
    {
        EditPersonPicture GetDetails(long id);
        List<PersonPictureViewModel> GetList();
        List<PersonPictureViewModel> Search(PersonPictureSearchModel searchModel);
    }
}
