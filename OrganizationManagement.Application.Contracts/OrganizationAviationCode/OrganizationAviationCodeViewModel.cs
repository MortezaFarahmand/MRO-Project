namespace BasicDatanManagement.Application.Contracts.OrganizationAviationCode
{
    public class OrganizationAviationCodeViewModel
    {
        public long Id { get; set; }
        public string ICAO { get; set; }
        public string IATA { get; set; }
        public string CivilAutority { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public string CallSign { get; set; }
        public long CountryId { get; set; }
        public string Country { get; set; }
        public bool IsEnabled { get; set; }
        public string CreationDate { get; set; }

    }

}
