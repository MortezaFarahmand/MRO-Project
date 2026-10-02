namespace BasicDataManagement.Application.Contracts.Airport
{
    public class CreateAirport
    {
        public string Ident { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public float Latitude_deg { get; set; }
        public float Longitude_deg { get; set; }
        public int Elevation_ft { get; set; }
        public string Continent { get; set; }
        public string Iso_country { get; set; }
        public string Iso_region { get; set; }
        public string Municipality { get; set; }
        public bool Scheduled_service { get; set; }
        public string Icao_code { get; set; }
        public string Iata_code { get; set; }
        public string Gps_code { get; set; }
        public string Local_code { get; set; }
        public string Home_link { get; set; }
        public string Wikipedia_link { get; set; }
        public string Keywords { get; set; }
    }
}
