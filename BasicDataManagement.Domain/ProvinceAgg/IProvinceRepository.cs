using _0_Framework.Domain;
using BasicDataManagement.Application.Contract.Province;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.ProvinceAgg
{
    public interface IProvinceRepository : IRepository<long, Province> 
    {
        EditProvince GetDetails(long id);
        List<ProvinceViewModel> Search(ProvinceSearchModel searchModel);
    }
}
