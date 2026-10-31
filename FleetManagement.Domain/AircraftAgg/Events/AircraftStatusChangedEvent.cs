using _0_Framework.Domain;
using FleetManagement.Domain.AircraftAgg.Enums;

public class AircraftStatusChangedEvent : DomainEventBase
{
    public long AircraftId { get; }
    public string Registration { get; }
    public AircraftStatus NewStatus { get; }

    public AircraftStatusChangedEvent(long aircraftId, string registration, AircraftStatus newStatus)
    {
        AircraftId = aircraftId;
        Registration = registration;
        NewStatus = newStatus;
    }
}