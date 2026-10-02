namespace BasicDataManagement.Application.Contracts.Airport
{
    public class AirportViewModel
    {
        public long Id { get; set; }
        public string Ident { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Elevation_ft { get; set; }
        public string Continent { get; set; }
        public string Iso_country { get; set; }
        public string Iso_region { get; set; }
        public string Municipality { get; set; }
        public string Icao_code { get; set; }
        public string Iata_code { get; set; }
        public string Local_code { get; set; }
        public string Home_link { get; set; }
        public string CreationDate { get; set; }
        public  bool IsActive { get; set; }
    }
}
