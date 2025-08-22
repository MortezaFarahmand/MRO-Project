using _0_Framework.Domain;
using OrganizationManagement.Application.Contracts.Country;
using System.Collections.Generic;

namespace OrganizationManagement.Domain.CountryAgg
{
    public interface IPersonRepository : IRepository<long, Country>
    {
        List<CountryViewModel> GetCountrys();
        EditPerson GetDetails(long id);
        List<CountryViewModel> Search(PersonSearchModel searchModel);
    }
}
