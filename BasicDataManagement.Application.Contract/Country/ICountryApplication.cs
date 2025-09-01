using System.Collections.Generic;
using _0_Framework.Application;

namespace BasicDataManagement.Application.Contracts.Country
{
    public interface ICountryApplication
    {
        OperationResult Create(CreateCountry command);
        OperationResult Edit(EditCountry command);
        EditCountry GetDetails(long id);
        List<CountryViewModel> GetCountrys();
        List<CountryViewModel> Search(CountrySearchModel searchModel);
    }
}
