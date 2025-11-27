using _0_Framework.Application;
using System.Collections.Generic;

namespace PersonnelManagement.Application.Contracts.PersonPicture
{
    public interface IPersonPictureApplication
    {
        OperationResult Create(CreatePersonPicture cammand);
        OperationResult Edit(EditPersonPicture command);
        EditPersonPicture GetDetails(long id);
        List<PersonPictureViewModel> GetPersonPictures();
        List<PersonPictureViewModel> Search(PersonPictureSearchModel searchModel);
    }
}
