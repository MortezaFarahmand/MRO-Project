using _0_Framework.Domain;
using OrganizationManagement.Domain.CountryAgg;
using OrganizationManagement.Domain.OrganizationAgg;
using OrganizationManagement.Domain.OrganizationPictureAgg;
using System.Collections.Generic;

namespace OrganizationManagement.Domain.ApprovalAutorityAgg
{
    public class ApprovalAuthority : EntityBase
    {
        public string NameEn { get; private set; }
        public string NameFa { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public string WebSite { get; private set; }
        public string Remark { get; private set; }
        public string Code { get; private set; }
        public string LogoPicture { get; private set; }
        public string LogoPictureAlt { get; private set; }
        public string LogoPictureTitle { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public string CanonicalAddress { get; private set; }
        public string Keywords { get; private set; }
        public long CountryId { get; private set; }
        public Country Country { get; private set; }
        public List<OrganizationPicture> OrganizationPictures { get; private set; }


        public ApprovalAuthority(string nameEn, string nameFa, string description, string address, string webSite, 
            string remark, string code, string logoPicture, string logoPictureAlt, string logoPictureTitle,
            string metaDescription, string slug, string canonicalAddress, string keywords, long countryId)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            Description = description;
            Address = address;
            WebSite = webSite;
            Remark = remark;
            Code = code;
            LogoPicture = logoPicture;
            LogoPictureAlt = logoPictureAlt;
            LogoPictureTitle = logoPictureTitle;
            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
            CountryId = countryId;
        }

        public void Edit(string nameEn, string nameFa, string description, string address, string webSite,
            string remark, string code, string logoPicture, string logoPictureAlt, string logoPictureTitle,
            string metaDescription, string slug, string canonicalAddress, string keywords, long countryId)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            Description = description;
            Address = address;
            WebSite = webSite;
            Remark = remark;
            Code = code;

            if (!string.IsNullOrWhiteSpace(logoPicture))
                LogoPicture = logoPicture;

            LogoPictureAlt = logoPictureAlt;
            LogoPictureTitle = logoPictureTitle;

            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
            CountryId = countryId;
        }
    }
}
