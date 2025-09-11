using _0_Framework.Domain;
using BasicDataManagement.Application.Contracts.Province;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.ProvinceAgg
{
    public interface IProvinceRepository : IRepository<long, Province> 
    {
        List<ProvinceViewModel> GetProvinces();
        EditProvince GetDetails(long id);
        List<ProvinceViewModel> Search(ProvinceSearchModel searchModel);
    }
}
