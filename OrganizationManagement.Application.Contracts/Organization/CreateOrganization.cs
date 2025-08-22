
using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.OrganizationGroup;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationManagement.Application.Contracts.Organization
{
    public class CreateOrganization
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string NameEn { get; set; }

        public string NameFa { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
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
        public long OrganizationAviationCodeId { get; set; }
        public string CompanyRegisterNo { get; set; }
        public string FormedDate { get; set; }
        public long ParentId { get; set; }
        public string LogoPicture { get; set; }
        public string LogoPictureAlt { get; set; }
        public string LogoPictureTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string CanonicalAddress { get; set; }
        public string Keywords { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long OrganizationGroupId { get; set; }

        public List<OrganizationGroupViewModel> Groups { get; set; }

    }
}
