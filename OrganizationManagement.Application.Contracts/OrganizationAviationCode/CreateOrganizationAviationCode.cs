
using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace BasicDatanManagement.Application.Contracts.OrganizationAviationCode
{
    public class CreateOrganizationAviationCode
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ICAO { get; set; }
        public string IATA { get; set; }
        public string CivilAutority { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public string CallSign { get; set; }
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long CountryId { get; set; }
        //public List<CountryViewModel> Countrys { get; set; }
    }

}
