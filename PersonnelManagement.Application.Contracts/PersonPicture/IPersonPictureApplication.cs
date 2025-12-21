using _0_Framework.Application;
using System.Collections.Generic;

namespace PersonnelManagement.Application.Contracts.PersonPicture
{
    public interface IPersonPictureApplication
    {
        OperationResult Create(CreatePersonPicture cammand);
        OperationResult Edit(EditPersonPicture command);
        EditPersonPicture GetDetails(long id);
        List<PersonPictureViewModel> GetList();
        List<PersonPictureViewModel> Search(PersonPictureSearchModel searchModel);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
    }
}
