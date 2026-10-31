namespace FleetManagement.Application.Contracts.Aircraft.Commands
{
    public class UpdateAircraftLifeCommand
    {
        public long Id { get; set; }
        public decimal TotalFlightHours { get; set; }
        public int TotalCycles { get; set; }
        public int TotalLandings { get; set; }
    }
}