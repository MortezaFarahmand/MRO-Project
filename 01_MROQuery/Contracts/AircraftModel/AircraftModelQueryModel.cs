
namespace _01_MROQuery.Contracts.AircraftModel
{
    public class AircraftModelQueryModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long AircraftTypeId { get; set; }
        public string AircraftTypeName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Display {  get; set; }
    }
}
