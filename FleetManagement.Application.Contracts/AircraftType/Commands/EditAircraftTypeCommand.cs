namespace FleetManagement.Application.Contracts.AircraftType.Commands
{
    public class EditAircraftTypeCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
