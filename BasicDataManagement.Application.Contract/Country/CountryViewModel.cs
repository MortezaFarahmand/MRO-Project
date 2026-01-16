using BasicDataManagement.Application.Contracts.Province;
using System.Collections.Generic;

namespace BasicDataManagement.Application.Contracts.Country
{
    public class CountryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string UNCode { get; set; }
        public string DialCode { get; set; }
        public string Picture { get; set; }
        public string TailCode { get; set; }
        public string CreationDate { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public List<ProvinceViewModel> Provinces { get; set; }
    }
}
