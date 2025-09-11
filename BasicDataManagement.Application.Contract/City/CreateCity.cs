using BasicDataManagement.Application.Contracts.Province;
using System.Collections.Generic;

namespace BasicDataManagement.Application.Contracts.City
{
    public class CreateCity
    {
        public string Name { get; set; }
        public string DialCode { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public long ProvinceId { get; set; }
        public List<ProvinceViewModel> Provincies { get; set; }
    }
}
