using _0_Framework.Domain;
using BasicDataManagement.Domain.ProvinceAgg;
namespace BasicDataManagement.Domain.CityAgg
{
    public class City : EntityBase
    {
        public string Name { get; private set; }
        public string DialCode { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public long ProvinceId { get; private set; }
        public Province Province { get; private set; }


        public City(string name, string dialCode, string metaDescription, string slug,
            string keywords, long provinceId)
        {
            Name = name;
            DialCode = dialCode;
            MetaDescription = metaDescription;
            this.Slug = slug;
            Keywords = keywords;
            ProvinceId = provinceId;
        }

        public void Edit(string name, string dialCode, string metaDescription, string slug,
            string keywords, long provinceId)
        {
            Name = name;
            DialCode = dialCode;
            MetaDescription = metaDescription;
            Slug = slug;
            Keywords = keywords;
            ProvinceId = provinceId;
        }
    }
}
