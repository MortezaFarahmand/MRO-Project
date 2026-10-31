using FleetManagement.Domain.AircraftAgg.Enums;

namespace FleetManagement.Application.Contracts.Aircraft.Commands
{
    public class ChangeAircraftStatusCommand
    {
        public long Id { get; set; }
        public AircraftStatus Status { get; set; }
    }
}
