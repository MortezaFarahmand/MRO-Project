
namespace FleetManagement.Application.Contracts.AircraftModel.DTOs
{
    public class AircraftModelDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long AircraftTypeId { get; set; }
        public string AircraftTypeName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
