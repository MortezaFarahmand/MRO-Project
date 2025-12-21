

using System;

namespace PersonnelManagement.Application.Contracts.Person
{
    public class PersonViewModel
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameFa { get; set; }
        public string FamilyEn { get; set; }
        public string FamilyFa { get; set; }
        public string FatherName { get; set; }
        public string Birthday { get; set; }
        public string PassportNo { get; set; }
        public string NationalCode { get; set; }
        public bool Activate { get; set; }
        public long PersonGroupId { get; set; }
        public string PersonGroup { get; set; }
        public long OrganizationId { get; set; }
        public string Organization { get; set; }
        public long BirthCityId { get; set; }
        public string BirthCity { get; set; }
        public long BirthProvinceId { get; set; }
        public string BirthProvince { get; set; }
        public long BirthCountryId { get; set; }
        public string BirthCountry { get; set; }
        public string WorkingStartDate { get; set; }
        public string MobileNo1 { get; set; }
        public string ProfilePicture { get; set; }
    }
}
