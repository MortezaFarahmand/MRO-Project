using _0_Framework.Domain;

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
        public string Birthday { get; private set; }
        public bool Marriage { get; private set; }
        public string PassportNo { get; private set; }
        public string NationalCode { get; private set; }
        public string NationalCodeOfFather { get; private set; }
        public long BirthCityId { get; private set; }
        public long CityId { get; private set; }
        public long CountryId { get; private set; }
        public string Attachment { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string IDCartNo { get; private set; }
        public string Address { get; private set; }
        public string MobileNo1 { get; private set; }
        public string MobileNo2 { get; private set; }
        public string PhoneNo { get; private set; }
        public string MailBoxAddress1 { get; private set; }
        public string MailBoxAddress2 { get; private set; }
        public string SocialAddress1 { get; private set; }
        public string SocialAddress2 { get; private set; }
        public string WorkingStartDate { get; private set; }
        public bool IsActive { get; private set; }
        public long PersonGroupId { get; private set; }
        public long OrganizationId { get; private set; }
        //public List<Country> Countrys { get; private set; }
        //public Person()
        //{
        //    Countrys = new List<Country>();
        //}

        public Person(string nameEn, string nameFa, string familyEn, string familyFa, string fatherName, string birthDay,
            string passportNo, string nationalCode, string attachment, string picture, string pictureAlt, string iDCartNo,
             string address, long organizationId, bool Activate)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            FamilyEn = familyEn;
            FamilyFa = familyFa;
            Birthday = birthDay;
            PassportNo = passportNo;
            NationalCode = nationalCode;
            Attachment = attachment;
            Picture = picture;
            PictureAlt = pictureAlt;
            IDCartNo = iDCartNo;
            Address = address;
            OrganizationId = organizationId;
            Activate = true;
        }

        public void Edit(string nameEn, string nameFa, string familyEn, string familyFa, string fatherName, string birthDay,
            string passportNo, string nationalCode, string attachment, string picture, string pictureAlt, string iDCartNo,
             string address, long organizationId)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            FamilyEn = familyEn;
            FamilyFa = familyFa;
            Birthday = birthDay;
            PassportNo = passportNo;
            NationalCode = nationalCode;
            Attachment = attachment;
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            IDCartNo = iDCartNo;
            Address = address;
            OrganizationId = organizationId;
        }

        //public void IsActive()
        //{
        //    this.Activate = true;
        //}

        //public void IsNotActive()
        //{
        //    this.Activate = false;
        //}
    }
}
