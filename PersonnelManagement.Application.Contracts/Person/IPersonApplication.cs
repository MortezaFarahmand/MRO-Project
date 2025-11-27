using System.Collections.Generic;
using _0_Framework.Application;

namespace PersonnelManagement.Application.Contracts.Person
{
    public interface IPersonApplication
    {
        OperationResult Create(CreatePerson command);
        OperationResult Edit(EditPerson command);
        EditPerson GetDetails(long id);
        List<PersonViewModel> GetPerson();
        List<PersonViewModel> Search(PersonSearchModel searchModel);
        OperationResult Active(long id);
        OperationResult DeActive(long id);
    }
}
