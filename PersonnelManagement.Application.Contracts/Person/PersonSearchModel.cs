namespace PersonnelManagement.Application.Contracts.Person
{
    public class PersonSearchModel
    {
        public string NameEn { get; set; }
        public string NameFa { get; set; }
        public string FamilyEn { get; set; }
        public string FamilyFa { get; set; }
        public string NationalCode { get; set; }
        public string IDCartNo { get; set; }
        public string PassportNo { get; set; }
        public long PersonGroupId { get; set; }
        public long OrganizationId { get; set; }
        public long BirthCityId { get; set; }
        public long BirthProvinceId { get; set; }
        public long BirthCountryId { get; set; }
        public string Birthday { get; set; }
        public string WorkingStartDate { get; set; }
    }
}
