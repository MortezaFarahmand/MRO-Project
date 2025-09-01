using _0_Framework.Domain;
using BasicDataManagement.Application.Contracts.Country;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.CountryAgg
{
    public interface ICountryRepository : IRepository<long, Country>
    {
        List<CountryViewModel> GetCountrys();
        EditCountry GetDetails(long id);
        List<CountryViewModel> Search(CountrySearchModel searchModel);
    }
}
