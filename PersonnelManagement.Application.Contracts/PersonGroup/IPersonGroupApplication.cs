using _0_Framework.Application;
using System.Collections.Generic;

namespace PersonnelManagement.Application.Contracts.PersonGroup
{
    public interface IPersonGroupApplication
    {
        OperationResult Create(CreatePersonGroup cammand);
        OperationResult Edit(EditPersonGroup command);
        EditPersonGroup GetDetails(long id);
        List<PersonGroupViewModel> GetPersonGroups();
        //List<PersonGroupViewModel> Search(PersonGroupSearchModel searchModel);
    }
}
