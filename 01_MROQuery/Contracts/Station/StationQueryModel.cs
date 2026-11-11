using _01_MROQuery.Contracts.AircraftModel;
using BasicDataManagement.Domain.ProvinceAgg;

namespace _01_MROQuery.Contracts.Station
{
    public class StationQueryModel
    {
        public long Id { get; set; }
        public string Ident { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Icao_code { get; set; }
        public string Iata_code { get; set; }
        public string Local_code { get; set; }
        public bool IsActive { get; set; }
        public string Display { get; set; }
    }
}
