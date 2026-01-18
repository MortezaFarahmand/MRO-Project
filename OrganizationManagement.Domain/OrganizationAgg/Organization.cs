using _0_Framework.Domain;
using BasicDataManagement.Domain.EntitiAgg;
using OrganizationManagement.Domain.OrganizationDepartmentAgg;
using OrganizationManagement.Domain.OrganizationGroupAgg;
using OrganizationManagement.Domain.OrganizationPictureAgg;
//using OrganizationManagement.Domain.PersonAgg;
using System;
using System.Collections.Generic;

namespace OrganizationManagement.Domain.OrganizationAgg
{
    public class Organization : EntityBase
    {
        public string NameEn { get; private set; }
        public string NameFa { get; private set; }
        public string DescriptionEn { get; private set; }
        public string DescriptionFa { get; private set; }
        public string AddressEn { get; private set; }
        public string AddressFa { get; private set; }
        public string Tel1 { get; private set; }
        public string Tel2 { get; private set; }
        public string Fax { get; private set; }
        public string WebSite { get; private set; }
        public string SocialAddress1 { get; private set; }
        public string SocialAddress2 { get; private set; }
        public string MailBox { get; private set; }
        public string Note { get; private set; }
        public string Remark { get; private set; }
        public long OrganizationAviationCodeId { get; private set; }
        public string CompanyRegisterNo { get; private set; }
        public DateTime FormedDate { get; private set; }
        public long ParentId { get; private set; }
        public bool IsActive { get; private set; }
        public string LogoPicture { get; private set; }
        public string LogoPictureAlt { get; private set; }
        public string LogoPictureTitle { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public string CanonicalAddress { get; private set; }
        public string Keywords { get; private set; }
        public long OrganizationGroupId { get; private set; }   
        //Approval List<Approval>
        public OrganizationGroup Group { get; private set; }
        public List<OrganizationPicture> OrganizationPictures { get; private set; }
        public long EntitiId { get; private set; }
        public Entiti Entiti {  get; private set; }
        //public List<Person> Persons { get; private set; } = new List<Person>();
        public List<OrganizationDepartment> OrganizationDepartments { get; private set; }


        public Organization(string nameEn, string nameFa, string descriptionEn, string descriptionFa, string addressEn,
            string addressFa, string tel1, string tel2, string fax, string webSite, string socialAddress1, string socialAddress2,
            string mailBox, string note, string remark, long organizationAviationCodeId, string companyRegisterNo, DateTime formedDate,
            long parentId, string logoPicture, string logoPictureAlt, string logoPictureTitle, string metaDescription, string slug,
            string canonicalAddress, string keywords, long organizationGroupId, long entitiId)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            DescriptionEn = descriptionEn;
            DescriptionFa = descriptionFa;
            AddressEn = addressEn;
            AddressFa = addressFa;
            Tel1 = tel1;
            Tel2 = tel2;
            Fax = fax;
            WebSite = webSite;
            SocialAddress1 = socialAddress1;
            SocialAddress2 = socialAddress2;
            MailBox = mailBox;
            Note = note;
            Remark = remark;
            OrganizationAviationCodeId = organizationAviationCodeId;
            CompanyRegisterNo = companyRegisterNo;
            FormedDate = formedDate;
            ParentId = parentId;
            IsActive = true;
            LogoPicture = logoPicture;
            LogoPictureAlt = logoPictureAlt;
            LogoPictureTitle = logoPictureTitle;
            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
            OrganizationGroupId = organizationGroupId;
            EntitiId = entitiId;
        }

        public void Edit(string nameEn, string nameFa, string descriptionEn, string descriptionFa, string addressEn,
            string addressFa, string tel1, string tel2, string fax, string webSite, string socialAddress1, string socialAddress2,
            string mailBox, string note, string remark, long organizationAviationCodeId, string companyRegisterNo, DateTime formedDate,
        long parentId, string logoPicture, string logoPictureAlt, string logoPictureTitle, string metaDescription, string slug,
            string canonicalAddress, string keywords, long organizationGroupId,long entitiId)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            DescriptionEn = descriptionEn;
            DescriptionFa = descriptionFa;
            AddressEn = addressEn;
            AddressFa = addressFa;
            Tel1 = tel1;
            Tel2 = tel2;
            Fax = fax;
            WebSite = webSite;
            SocialAddress1 = socialAddress1;
            SocialAddress2 = socialAddress2;
            MailBox = mailBox;
            Note = note;
            Remark = remark;
            OrganizationAviationCodeId = organizationAviationCodeId;
            CompanyRegisterNo = companyRegisterNo;
            FormedDate = formedDate;
            ParentId = parentId;
            LogoPicture = logoPicture;
            LogoPictureAlt = logoPictureAlt;
            LogoPictureTitle = logoPictureTitle;
            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
            OrganizationGroupId= organizationGroupId;
            EntitiId = entitiId;
        }

        public void Active()
        {
            IsActive = true;
        }

        public void NotActive()
        {
            IsActive = false;
        }

    }
}
