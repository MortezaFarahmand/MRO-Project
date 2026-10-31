using _0_Framework.Domain;

public class AircraftCreatedEvent : DomainEventBase
{
    public long AircraftId { get; }
    public string Registration { get; }
    public string SerialNumber { get; }

    public AircraftCreatedEvent(long aircraftId, string registration, string serialNumber)
    {
        AircraftId = aircraftId;
        Registration = registration;
        SerialNumber = serialNumber;
    }
}
