namespace FleetManagement.Application.Contracts.AircraftModel.Queries
{
    public class SearchAircraftModelQuery
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public long? AircraftTypeId { get; set; }
        public string Manufacturer { get; set; }
        public bool? IsActive { get; set; }
    }
}
