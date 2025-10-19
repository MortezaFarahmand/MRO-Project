using _0_Framework.Domain;
using BasicDataManagement.Application.Contracts.City;
using BasicDataManagement.Application.Contracts.Country;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.CityAgg
{
    public interface ICityRepository : IRepository<long, City>
    {
        List<CityViewModel> GetCitys();
        EditCity GetDetails(long id);
        List<CityViewModel> Search(CitySearchModel searchModel);
        
    }
}
