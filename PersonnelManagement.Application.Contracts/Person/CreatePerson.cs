
using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.City;
using BasicDataManagement.Application.Contracts.Country;
using BasicDataManagement.Application.Contracts.Province;
using PersonnelManagement.Application.Contracts.PersonGroup;
using PersonnelManagement.Application.Contracts.PersonPicture;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Application.Contracts.Person
{
    public class CreatePerson
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string NameEn { get; set; }

        public string NameFa { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string FamilyEn { get; set; }

        public string FamilyFa { get; set; }
        public string FatherName { get; set; }
        public bool Gender { get; set; }
        public string Birthday { get; set; }
        public bool Marriage { get; set; }

        public string PassportNo { get; set; }

        public string NationalCode { get; set; }

        public string NationalCodeOfFather { get; set; }
        public long BirthCityId { get; set; }
        public long BirthProvinceId { get; set; }
        public long BirthCountryId { get; set; }
        public long CityId { get; set; }
        public string Attachment { get; set; }
        public string EducationalDegree { get; set; }
        public string EducationalField { get; set; }
        public string IDCartNo { get; set; }
        public long AddressId { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string PhoneNo { get; set; }
        public string MailBoxAddress1 { get; set; }
        public string MailBoxAddress2 { get; set; }
        public string SocialAddress1 { get; set; }
        public string SocialAddress2 { get; set; }
        public string WorkingStartDate { get; set; }
        public long PersonGroupId { get; set; }
        public List<PersonGroupViewModel> Groups { get; set; }
        public long OrganizationId { get; set; }
        public List<CountryViewModel> Countrys { get; set; }
        public List<ProvinceViewModel> Provinces { get; set; }
        public List<CityViewModel> Cities { get; set; }

        [FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        public string ProfilePicture { get; set; }

        public List<PersonPictureViewModel> PersonPictures { get; set; }
        //[Required(ErrorMessage=ValidationMessages.IsRequired)]
        //[FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        //[MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]

    }
}
