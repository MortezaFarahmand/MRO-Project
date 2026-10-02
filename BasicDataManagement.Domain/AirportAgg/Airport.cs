using _0_Framework.Domain;
using System.Data.Common;
namespace BasicDataManagement.Domain.AirportAgg
{
    public class Airport : EntityBase
    {
        public string  Ident { get; private set; }
        public string Type { get; private set; }
        public  string Name { get; private set; }
        public float Latitude_deg { get; private set; }
        public float Longitude_deg { get; private set;}
        public int Elevation_ft  { get; private set; }
        public string Continent { get; private set; }
        public string Iso_country { get; private set; }
        public string Iso_region { get; private set; }
        public string Municipality { get; private set; }
        public bool Scheduled_service { get; private set; }
        public string Icao_code { get; private set; }
        public string Iata_code { get; private set; }
        public string Gps_code { get; private set; }
        public string Local_code { get; private set; }
        public string Home_link { get; private set; }
        public string Wikipedia_link { get; private set; }
        public string Keywords { get; private set; }
        public bool IsActive { get; private set; }

        public Airport()
        {
            
        }

        public Airport(string ident, string type, string name, float latitude_deg, float longitude_deg, 
            int elevation_ft, string continent, string iso_country, string iso_region, 
            string municipality, bool scheduled_service, string icao_code, 
            string iata_code, string gps_code, string local_code, string home_link, 
            string wikipedia_link, string keywords)
        {
            Ident = ident;
            Type = type;
            Name = name;
            Latitude_deg = latitude_deg;
            Longitude_deg = longitude_deg;
            Elevation_ft = elevation_ft;
            Continent = continent;
            Iso_country = iso_country;
            Iso_region = iso_region;
            Municipality = municipality;
            Scheduled_service = scheduled_service;
            Icao_code = icao_code;
            Iata_code = iata_code;
            Gps_code = gps_code;
            Local_code = local_code;
            Home_link = home_link;
            Wikipedia_link = wikipedia_link;
            Keywords = keywords;
            IsActive = true;
        }

        public void Edit(string ident, string type, string name, float latitude_deg, float longitude_deg,
            int elevation_ft, string continent, string iso_country, string iso_region,
            string municipality, bool scheduled_service, string icao_code,
            string iata_code, string gps_code, string local_code, string home_link,
            string wikipedia_link, string keywords)
        {
            Ident = ident;
            Type = type;
            Name = name;
            Latitude_deg = latitude_deg;
            Longitude_deg = longitude_deg;
            Elevation_ft = elevation_ft;
            Continent = continent;
            Iso_country = iso_country;
            Iso_region = iso_region;
            Municipality = municipality;
            Scheduled_service = scheduled_service;
            Icao_code = icao_code;
            Iata_code = iata_code;
            Gps_code = gps_code;
            Local_code = local_code;
            Home_link = home_link;
            Wikipedia_link = wikipedia_link;
            Keywords = keywords;
        }

        public void Active()
        {
            IsActive = true;
        }

        public void Deactive() 
        { 
            IsActive = false;
        }
    }
}
