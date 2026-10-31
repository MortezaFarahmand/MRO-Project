namespace FleetManagement.Application.Contracts.AircraftType.Commands
{
    public class CreateAircraftTypeCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
