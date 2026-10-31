namespace FleetManagement.Application.Contracts.AircraftModel.Commands
{
    public class EditAircraftModelCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
    }
}
