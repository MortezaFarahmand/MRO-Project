using _0_Framework.Domain;

namespace FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg
{
    public interface IAircraftModelRepository : IRepository<long, AircraftModel>
    {
        AircraftModel GetByCode(string code);
        bool ExistsCode(string code);
        List<AircraftModel> GetByTypeId(long aircraftTypeId);
        List<AircraftModel> Search(string code, string name, long? typeId, string manufacturer, bool? isActive = null);
    }
}