using _0_Framework.Application;
using System.Collections.Generic;

namespace BasicDataManagement.Application.Contract.Province
{
    public interface IProvinceApplication
    {
        OperationResult Create(CreateProvince command);
        OperationResult Edit(EditProvince command);
        EditProvince GetDetails(long id);
        List<ProvinceViewModel> GetProvinces();
        List<ProvinceViewModel> Search(ProvinceSearchModel searchModel); 
    }
}
