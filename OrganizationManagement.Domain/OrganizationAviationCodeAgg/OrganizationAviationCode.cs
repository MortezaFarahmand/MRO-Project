using _0_Framework.Domain;

namespace OrganizationManagement.Domain.OrganizationAviationCodeAgg
{
    public class OrganizationAviationCode : EntityBase
    {
        public string ICAO { get; private set; }
        public string IATA { get; private set; }
        public string CivilAutority { get; private set; }
        public string Comment { get; private set; }
        public string Description { get; private set; }
        public string CallSign { get; private set; }
        public long CountryId { get; private set; }
        public bool IsEnabled { get; private set; }
        
        

        public OrganizationAviationCode(string iCAO, string iATA, string civilAutority, string comment,
            string description, string callSign, long countryId)
        {
            ICAO = iCAO;
            IATA = iATA;
            CivilAutority = civilAutority;
            Comment = comment;
            Description = description;
            CallSign = callSign;
            CountryId = countryId;
            IsEnabled = true;
        }

        public void Edit(string iCAO, string iATA, string civilAutority, string comment,
            string description, string callSign, long countryId)
        {
            ICAO= iCAO;
            IATA= iATA;
            CivilAutority = civilAutority;
            Comment = comment;  
            Description = description;  
            CallSign = callSign;
            CountryId = countryId;
        }

        public void Enabled()
        {
            IsEnabled = true;
        }

        public void Disabled() 
        { 
            IsEnabled = false;
        }
    }
}
