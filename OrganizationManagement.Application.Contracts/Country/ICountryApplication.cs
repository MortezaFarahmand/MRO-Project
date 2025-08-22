using System.Collections.Generic;
using _0_Framework.Application;

namespace OrganizationManagement.Application.Contracts.Country
{
    public interface IPersonApplication
    {
        OperationResult Create(CreatePerson command);
        OperationResult Edit(EditPerson command);
        EditPerson GetDetails(long id);
        List<CountryViewModel> GetCountrys();
        List<CountryViewModel> Search(PersonSearchModel searchModel);
    }
}
