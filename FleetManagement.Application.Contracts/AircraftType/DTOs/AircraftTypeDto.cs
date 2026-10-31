
namespace FleetManagement.Application.Contracts.AircraftType.DTOs
{
    public class AircraftTypeDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
