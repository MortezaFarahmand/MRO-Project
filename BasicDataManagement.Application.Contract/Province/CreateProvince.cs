
namespace BasicDataManagement.Application.Contract.Province
{
    public class CreateProvince
    {
        public string Name { get; set; }
        public string DialCode { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public long CountryId { get; set; }
    }
}
