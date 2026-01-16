using _0_Framework.Domain;
using BasicDataManagement.Domain.CityAgg;
using BasicDataManagement.Domain.CountryAgg;
using BasicDataManagement.Domain.ProvinceAgg;
using PersonnelManagement.Domain.PersonGroupAgg;
using PersonnelManagement.Domain.PersonPictureAgg;
using System;
using System.Collections.Generic;

namespace PersonnelManagement.Domain.PersonAgg
{
    public class Person : EntityBase
    {
        public string NameEn { get; private set; }
        public string NameFa { get; private set; }
        public string FamilyEn { get; private set; }
        public string FamilyFa { get; private set; }
        public string FatherName { get; private set; }
        public bool Gender { get; private set; }
        public DateTime Birthday { get; private set; }
        public bool Marriage { get; private set; }
        public string PassportNo { get; private set; }
        public string NationalCode { get; private set; }
        public string NationalCodeOfFather { get; private set; }
        public long BirthCityId { get; private set; }
        public long BirthProvinceId { get; private set; }//
        public long BirthCountryId { get; private set; }//
        public long CityId { get; private set; }
        public string Attachment { get; private set; }
        public string EducationalDegree { get; private set; }
        public string EducationalField { get; private set; }
        public string IDCartNo { get; private set; }
        public long AddressId { get; private set; }
        public string MobileNo1 { get; private set; }
        public string MobileNo2 { get; private set; }
        public string PhoneNo { get; private set; }
        public string MailBoxAddress1 { get; private set; }
        public string MailBoxAddress2 { get; private set; }
        public string SocialAddress1 { get; private set; }
        public string SocialAddress2 { get; private set; }
        public DateTime WorkingStartDate { get; private set; }
        public bool Activate { get; private set; }
        public long PersonGroupId { get; private set; }
        public PersonGroup Group { get; private set; }
        public long OrganizationId { get; private set; }
        public List<Country> Countrys { get; private set; }
        public List<Province> Provinces { get; private set; }
        public List<City> Cities { get; private set; }
        public string ProfilePicture { get; private set; }
        public List<PersonPicture> PersonPictures { get; private set; }

        public Person()
        {
            Countrys = new List<Country>();
            Provinces = new List<Province>();
            Cities = new List<City>();
            //Groups = new List<PersonGroup>();
        }

        public Person(string nameEn, string nameFa, string familyEn, string familyFa, string fatherName,bool gender,
            DateTime birthDay, bool marriage, string passportNo, string nationalCode, string nationalCodeOfFather,
            long birthCityId, long birthProvinceId, long birthCountryId, long cityId, string attachment,string educationalDegree, string educationalFeild,
            string iDCartNo,long addressId,string mobileNo1, string mobileNo2,string phoneNo,string mailBoxAddress1,
            string mailBoxAddress2, string socialAddress1, string socialAddress2, DateTime workingStartDate,
            long personGroupId, long organizationId, string profilePicture)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            FamilyEn = familyEn;
            FamilyFa = familyFa;
            FatherName = fatherName;
            Gender = gender;
            Birthday = birthDay;
            Marriage = marriage;
            PassportNo = passportNo;
            NationalCode = nationalCode;
            NationalCodeOfFather = nationalCodeOfFather;
            BirthCityId = birthCityId;//
            BirthProvinceId = birthProvinceId;
            BirthCountryId = birthCountryId;
            CityId = cityId;//
            Attachment = attachment;
            EducationalDegree = educationalDegree;
            EducationalField = educationalFeild;
            IDCartNo = iDCartNo;
            AddressId = addressId;
            MobileNo1 = mobileNo1;
            MobileNo2 = mobileNo2;
            PhoneNo = phoneNo;
            MailBoxAddress1 = mailBoxAddress1;
            MailBoxAddress2 = mailBoxAddress2;
            SocialAddress1 = socialAddress1;
            SocialAddress2 = socialAddress2;
            WorkingStartDate = workingStartDate;
            Activate = true;
            PersonGroupId = personGroupId;//
            OrganizationId = organizationId;//
            if (!string.IsNullOrWhiteSpace(ProfilePicture))
                ProfilePicture = profilePicture;
            
        }

        public void Edit(string nameEn, string nameFa, string familyEn, string familyFa, string fatherName, bool gender,
            DateTime birthDay, bool marriage, string passportNo, string nationalCode, string nationalCodeOfFather,
            long birthCityId, long birthProvinceId, long birthCountryId, long cityId, string attachment, string educationalDegree, string educationalFeild,
            string iDCartNo, long addressId, string mobileNo1, string mobileNo2, string phoneNo, string mailBoxAddress1,
            string mailBoxAddress2, string socialAddress1, string socialAddress2, DateTime workingStartDate,
            long personGroupId, long organizationId, string profilePicture)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            FamilyEn = familyEn;
            FamilyFa = familyFa;
            FatherName = fatherName;
            Gender = gender;
            Birthday = birthDay;
            Marriage = marriage;
            PassportNo = passportNo;
            NationalCode = nationalCode;
            NationalCodeOfFather = nationalCodeOfFather;
            BirthCityId = birthCityId;//
            BirthProvinceId = birthProvinceId;
            BirthCountryId = birthCountryId;
            CityId = cityId;//
            Attachment = attachment;
            EducationalDegree = educationalDegree;
            EducationalField = educationalFeild;
            IDCartNo = iDCartNo;
            AddressId = addressId;
            MobileNo1 = mobileNo1;
            MobileNo2 = mobileNo2;
            PhoneNo = phoneNo;
            MailBoxAddress1 = mailBoxAddress1;
            MailBoxAddress2 = mailBoxAddress2;
            SocialAddress1 = socialAddress1;
            SocialAddress2 = socialAddress2;
            WorkingStartDate = workingStartDate;
            PersonGroupId = personGroupId;//
            OrganizationId = organizationId;//
            if (!string.IsNullOrWhiteSpace(ProfilePicture))
                ProfilePicture = profilePicture;

        }

        public void Active()
        {
            this.Activate = true;
        }

        public void DeActive()
        {
            this.Activate = false;
        }
    }
}
