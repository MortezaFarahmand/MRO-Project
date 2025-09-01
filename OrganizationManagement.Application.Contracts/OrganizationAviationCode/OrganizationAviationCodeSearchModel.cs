namespace BasicDatanManagement.Application.Contracts.OrganizationAviationCode
{
    public class OrganizationAviationCodeSearchModel
    {
        public string ICAO { get; set; }
        public string IATA { get; set; }
        public string CivilAutority { get; set; }
        public string Description { get; set; }
        public long CountryId { get; set; }
    }

}
