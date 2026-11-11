
using _01_MROQuery.Contracts.Common;

namespace _01_MROQuery.Contracts.Station
{
    public interface IStationQuery
    {
        List<StationQueryModel> GetAll();
        StationQueryModel GetById(long id);
        List<StationQueryModel> GetByCountryId(string isoCountry);
        //List<StationQueryModel> Search(string term, int take);
        List<LookupItemDto> Search(string term, int take = 30);
        LookupItemDto GetLookupById(long id);
        string GetDisplay(long stationId);
    }
}
