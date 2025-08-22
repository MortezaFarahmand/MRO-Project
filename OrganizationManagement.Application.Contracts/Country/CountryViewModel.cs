namespace OrganizationManagement.Application.Contracts.Country
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
        //public int OrganizationCount { get; set; }
    }
}
