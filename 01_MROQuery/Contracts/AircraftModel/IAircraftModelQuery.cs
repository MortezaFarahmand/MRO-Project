
namespace _01_MROQuery.Contracts.AircraftModel
{
    public interface IAircraftModelQuery
    {
        List<AircraftModelQueryModel> GetAll();
        AircraftModelQueryModel GetById(long id);
        List<AircraftModelQueryModel> GetByTypeId(long aircraftTypeId);
    }
}
