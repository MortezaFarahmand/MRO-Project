using _0_Framework.Domain;
using OrganizationManagement.Domain.ApprovalAutorityAgg;
using OrganizationManagement.Domain.OrganizationAviationCodeAgg;
using System.Collections.Generic;
namespace OrganizationManagement.Domain.CountryAgg
{
    public class Country : EntityBase
    {
        public string Name { get; private set; }
        public string Alpha2Code { get; private set; }
        public string Alpha3Code { get; private set; }
        public string UNCode { get; private set; }
        public string DialCode { get; private set; }
        public string Picture { get; private set; }
        public string TailCode { get; private set; }
        public List<OrganizationAviationCode> OrganizationAviationCodes { get; private set; }
        public ApprovalAuthority ApprovalAuthority { get; private set; }
        public Country()
        {
            OrganizationAviationCodes = new List<OrganizationAviationCode>();
        }


        public Country(string name, string alpha2Code, string alpha3Code, string uNCode,
            string dialCode, string picture, string tailCode)
        {
            Name = name;
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            UNCode = uNCode;
            DialCode = dialCode;
            Picture = picture;
            TailCode = tailCode;
        }

        public void Edit(string name, string alpha2Code, string alpha3Code, string uNCode,
           string dialCode, string picture, string tailCode)
        {
            Name = name;
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            UNCode = uNCode;
            DialCode = dialCode;
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            Picture = picture;
            TailCode = tailCode;
        }
    }
}

