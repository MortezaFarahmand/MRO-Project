using _0_Framework.Application;
using System.Collections.Generic;

namespace BasicDataManagement.Application.Contracts.City
{
    public interface ICityApplication
    {
        OperationResult Create(CreateCity command);
        OperationResult Edit(EditCity command);
        EditCity GetDetails(long id);
        List<CityViewModel> GetCitys();
        List<CityViewModel> Search(CitySearchModel searchModel); 
    }
}
