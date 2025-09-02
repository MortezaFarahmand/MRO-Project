using _0_Framework.Domain;
using BasicDataManagement.Domain.CountryAgg;
namespace BasicDataManagement.Domain.ProvinceAgg
{
    public class Province : EntityBase
    {
        public string Name { get; private set; }
        public string DialCode { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public long CountryId { get; private set; }
        public Country Country { get; private set; }

        public Province(string name, string dialCode, string metaDescription, string slug,
            string keywords, long countryId)
        {
            Name = name;
            DialCode = dialCode;
            MetaDescription = metaDescription;
            this.Slug = slug;
            Keywords = keywords;
            CountryId = countryId;
        }

        public void Edit(string name, string dialCode, string metaDescription, string slug,
            string keywords, long countryId)
        {
            Name = name;
            DialCode = dialCode;
            MetaDescription = metaDescription;
            Slug = slug;
            Keywords = keywords;
            CountryId = countryId;
        }
    }
}
