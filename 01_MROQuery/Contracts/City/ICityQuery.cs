
namespace _01_MROQuery.Contracts.City
{
    public interface ICityQuery
    {
        List<CityQueryModel> GetAll();
        CityQueryModel GetById(long id);
        List<CityQueryModel> GetByProvinceId(long provinceId);
    }
}
