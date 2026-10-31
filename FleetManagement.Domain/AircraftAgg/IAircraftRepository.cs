using _0_Framework.Domain;
using FleetManagement.Domain.AircraftAgg.Enums;

namespace FleetManagement.Domain.AircraftAgg
{
    public interface IAircraftRepository : IRepository<long, Aircraft>
    {
        Aircraft GetByRegistration(string registration);
        Aircraft GetBySerialNumber(string serialNumber);
        bool ExistsRegistration(string registration);
        bool ExistsSerialNumber(string serialNumber);
        List<Aircraft> Search(string registration, string serialNumber, long? modelId, long? organizationId, AircraftStatus? status);
    }
}