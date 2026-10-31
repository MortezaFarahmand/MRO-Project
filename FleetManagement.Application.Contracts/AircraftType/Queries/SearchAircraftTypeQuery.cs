namespace FleetManagement.Application.Contracts.AircraftType.Queries
{
    public class SearchAircraftTypeQuery
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
