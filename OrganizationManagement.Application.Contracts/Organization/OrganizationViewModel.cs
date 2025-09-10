
using System;

namespace OrganizationManagement.Application.Contracts.Organization
{
    public class OrganizationViewModel
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameFa { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionFa { get; set; }
        public string AddressEn { get; set; }
        public string AddressFa { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public string SocialAddress1 { get; set; }
        public string SocialAddress2 { get; set; }
        public string MailBox { get; set; }
        public string Note { get; set; }
        public string Remark { get; set; }
        public string CompanyRegisterNo { get; set; }
        public string FormedDate { get; set; }
        public DateTime FormedDateGr { get; set; }
        public string LogoPicture { get; set; }
        public long ParentId { get; set; } // public string Parent { get; set; }
        public long OrganizationAviationCodeId { get; set; }  //public string OrganizationAviationCode { get; set; }
        public string Group { get; set; }
        public long OrganizationGroupId { get; set; }
        public string CreationDate { get; set; }
        public bool IsActive { get; set; }
        //public string Country { get; set; }
    }
}
