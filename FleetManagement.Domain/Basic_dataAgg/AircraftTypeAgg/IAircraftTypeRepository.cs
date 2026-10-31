using _0_Framework.Domain;

namespace FleetManagement.Domain.Basic_dataAgg.AircraftTypeAgg
{
    public interface IAircraftTypeRepository : IRepository<long, AircraftType>
    {
        AircraftType GetByCode(string code);
        bool ExistsCode(string code);
        List<AircraftType> Search(string code, string name, bool? isActive = null);
    }
}