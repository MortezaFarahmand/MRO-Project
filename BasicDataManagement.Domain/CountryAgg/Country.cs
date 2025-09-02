using _0_Framework.Domain;
using BasicDataManagement.Domain.ProvinceAgg;
using System.Collections.Generic;
//using OrganizationManagement.Domain.ApprovalAutorityAgg;
//using OrganizationManagement.Domain.OrganizationAviationCodeAgg;
//using System.Collections.Generic;
namespace BasicDataManagement.Domain.CountryAgg
{
    public class Country : EntityBase
    {
        public string Name { get; private set; }
        public string Alpha2Code { get; private set; }
        public string Alpha3Code { get; private set; }
        public string UNCode { get; private set; }
        public string DialCode { get; private set; }
        public long PictureId { get; private set; }
        public string TailCode { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public List<Province> Provinces { get; private set; }
        public Country()
        {
            Provinces = new List<Province>();
        }
        //public List<OrganizationAviationCode> OrganizationAviationCodes { get; private set; }
        //public ApprovalAuthority ApprovalAuthority { get; private set; }
        //public Country()
        //{
        //    OrganizationAviationCodes = new List<OrganizationAviationCode>();
        //}


        public Country(string name, string alpha2Code, string alpha3Code, string uNCode,
            string dialCode, long pictureId, string tailCode, string metaDescription, string slug)
        {
            Name = name;
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            UNCode = uNCode;
            DialCode = dialCode;
            PictureId = pictureId;
            TailCode = tailCode;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, string alpha2Code, string alpha3Code, string uNCode,
           string dialCode, long pictureId, string tailCode, string metaDescription, string slug)
        {
            Name = name;
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            UNCode = uNCode;
            DialCode = dialCode;
            //if (!string.IsNullOrWhiteSpace(picture))
            //    Picture = picture;
            PictureId = pictureId;
            TailCode = tailCode;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}

